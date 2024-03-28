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
            foreach (var profession in personDTO.PrimaryProfessions)
            {
                var personalCareer = await _context.PersonalCareers.FindAsync(profession) ?? new PersonalCareer { PrimProf = profession };
                //var personalCareer = await _context.PersonalCareers.FirstOrDefaultAsync(pc => pc.PrimProf == profession) ?? new PersonalCareer { PrimProf = profession };

                person.PersonalCareers.Add(personalCareer);
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
    }
}
