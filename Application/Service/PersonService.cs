using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;


        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Person> CreatePersonAsync(Person person)
        {
            int age = DateTime.Now.Year - person.DateOfBirth.Year;
            if (person.DateOfBirth.Date > DateTime.Now.AddYears(-age)) age--;

            if (age > 150)
            {
                throw new ArgumentException("A person cannot be older than 150 years.");
            }

            return await _personRepository.CreatePersonAsync(person);
        }

        public async Task<IEnumerable<Job>> GetJobsByPersonIdAsync(int personId)
        {
            return await _personRepository.GetJobsByPersonIdAsync(personId);
        }
        public async Task<Person> GetPersonByIdAsync(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            return person;
        }
        public Task<IEnumerable<Person>> GetAllPersonsAsync()
        {
            return _personRepository.GetAllPersonsAsync();
        }

        public Task<IEnumerable<Job>> GetJobsBetweenDatesAsync(int personId, DateTime startDate, DateTime endDate)
        {
            return _personRepository.GetJobsBetweenDatesAsync(personId, startDate, endDate);
        }
    }
}
