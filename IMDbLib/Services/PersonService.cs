using IMDbLib.DataContext;
using IMDbLib.DTOs;
using IMDbLib.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbLib.Services
{
    public class PersonService
    {
        private readonly IMDb_Context _context;

        public PersonService(IMDb_Context context)
        {
            _context = context;
        }

        public async Task AddPerson(PersonDTO personDTO)
        {
            // Call the stored procedure to generate the new ID
            var idQuery = new SqlParameter("@newId", System.Data.SqlDbType.NVarChar)
            {
                Direction = System.Data.ParameterDirection.Output,
                Size = 400 // Set the size to a large enough value to hold the output
            };

            await _context.Database.ExecuteSqlRawAsync("EXECUTE dbo.GenerateNextPersonId @newId OUT", idQuery);

            string newId = idQuery.Value.ToString();

            // Create the Person object
            Person person = new Person
            {
                Nconst = newId,
                PrimaryName = personDTO.PrimaryName,
                BirthYear = personDTO.BirthYear,
                DeathYear = personDTO.DeathYear
            };

            // Check if the Professions exist, if not create them and add them to the Person
            foreach (var dbProfession in personDTO.PrimaryProfessions)
            {
                var professionEntity = await _context.Professions.FindAsync(dbProfession) ?? new Profession { PrimaryProfession = dbProfession };
                person.PersonalCareers.Add(new PersonalCareer { Person = person, Profession = professionEntity });

                // ELLER

                //var professionEntity = await _context.Professions.FindAsync(dbProfession) ?? new Profession { PrimaryProfession = dbProfession };
                //var personalCareer = new PersonalCareer { Person = person, Profession = professionEntity };
                //_context.PersonalCareers.Add(personalCareer);
            }

            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
        }


        public async Task<List<PersonDTO>> GetPersonListByName(string searchString)
        {
            // Call the stored procedure
            var persons = await _context.Persons
                .FromSqlInterpolated($"EXECUTE dbo.SearchPersons {searchString}")
                .ToListAsync();

            // Load the Professions collection for each person
            foreach (var person in persons)
            {
                _context.Entry(person).Collection(p => p.PersonalCareers).Load();
            }

            // Convert the Person objects to PersonDTOs
            var personDTOs = persons.Select(p => new PersonDTO
            {
                Nconst = p.Nconst,
                PrimaryName = p.PrimaryName,
                BirthYear = p.BirthYear,
                DeathYear = p.DeathYear,
                PrimaryProfessions = p.PersonalCareers.Select(pc => pc.PrimProf).ToList() 
            }).ToList();

            return personDTOs;
        }

        public async Task<AllPersonInfoDTO> GetAllPersonInfoByNconst(string nconst)
        {
            var person = await _context.Persons
                .Include(p => p.PersonalCareers)
                    .ThenInclude(pc => pc.Profession)
                .Include(p => p.Directors)
                    .ThenInclude(d => d.MovieBase)
                .Include(p => p.Writers)
                    .ThenInclude(w => w.MovieBase)
                .Include(p => p.KnownForTitles)
                    .ThenInclude(k => k.MovieBase)
                .FirstOrDefaultAsync(p => p.Nconst == nconst);

            if (person == null)
            {
                throw new Exception($"Person with ID {nconst} not found");
            }

            var personDTO = new AllPersonInfoDTO
            {
                Nconst = person.Nconst,
                PrimaryName = person.PrimaryName,
                BirthYear = person.BirthYear,
                DeathYear = person.DeathYear,
                PrimaryProfessions = person.PersonalCareers?.Select(pc => pc.Profession.PrimaryProfession).ToList() ?? new List<string>(),
                MovieDirectors = person.Directors?.Select(d => d.MovieBase.PrimaryTitle).ToList() ?? new List<string>(),
                MovieWriters = person.Writers?.Select(w => w.MovieBase.PrimaryTitle).ToList() ?? new List<string>(),
                KnownForTitles = person.KnownForTitles?.Select(k => k.MovieBase.PrimaryTitle).ToList() ?? new List<string>()
            };

            return personDTO;
        }


        /// <summary>
        /// Sletter en person fra databasen, via en STORED PROCEDURE.
        /// Alle Person relaterede conjuctions slettes også grundet CASCADE DELETE.
        /// </summary>
        /// <param name="nconst"></param>
        /// <returns></returns>
        public async Task DeletePerson(string nconst)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"EXECUTE dbo.DeletePerson {nconst}");
        }

        public async Task DeletePersonEF(string nconst)
        {
            // Find the person with the given nconst
            var person = await _context.Persons.FindAsync(nconst);

            if (person != null)
            {
                // Remove the person from the DbContext
                _context.Persons.Remove(person);

                // Save the changes to the database
                await _context.SaveChangesAsync();
            }
        }
    }
}
