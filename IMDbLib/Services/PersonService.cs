using IMDbLib.Models;
using IMDbLib.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbLib.Services
{
    public class PersonService
    {
        private readonly IGRepository<Person> _repository;

        public PersonService(IGRepository<Person> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public IEnumerable<Person> SearchPersons(string name)
        {
            return _repository.RunStoredProcedure("SearchPersons", name).Cast<Person>();
        }

        public void AddPerson(Person person)
        {
            _repository.RunStoredProcedure("AddPerson", person.PrimaryName, person.BirthYear, person.DeathYear);
        }

        public Person GetPersonDetails(string nconst)
        {
            return _repository.RunStoredProcedure("GetPersonDetails", nconst).Cast<Person>().FirstOrDefault();
        }

        public Person UpdatePerson(Person person)
        {
            _repository.RunStoredProcedure("UpdatePerson", person.Nconst, person.PrimaryName, person.BirthYear, person.DeathYear);
            return _repository.Get(person.Nconst);
        }

        public void DeletePerson(Person person)
        {
            _repository.RunStoredProcedure("DeletePerson", person.Nconst);
        }
    }
}
