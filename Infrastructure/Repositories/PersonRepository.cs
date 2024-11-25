using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PersonReposiroty : IPersonRepository
    {

        private readonly ApplicationDbContext _context;

        public PersonReposiroty(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Person>> GetAllPersonsAsync()
        {
            return await _context.Persons
                .Include(p => p.Jobs)
                .OrderBy(p => p.LastName)
                .ToListAsync();

        }
        public async Task<Person> CreatePersonAsync(Person person)
        {
            var age = DateTime.Now.Year - person.DateOfBirth.Year;
            if (age > 150)
                throw new ArgumentException("Person must be less than 150 years old.");

            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            return person;
        }
        public async Task<IEnumerable<Job>> GetJobsByPersonIdAsync(int personId)
        {
            return await _context.Jobs
                .Where(job => job.PersonId == personId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Job>> GetJobsBetweenDatesAsync(int personId, DateTime startDate, DateTime endDate)
        {
            var person = await _context.Persons
                .Include(p => p.Jobs)
                .FirstOrDefaultAsync(p => p.Id == personId);

            if (person == null)
                throw new KeyNotFoundException("Person not found.");

            return person.Jobs.Where(j => j.StartDate >= startDate && j.StartDate <= endDate).ToList();
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            return await _context.Persons
                .Include(p => p.Jobs)  
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
