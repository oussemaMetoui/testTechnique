using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class JobReposirtory : IJobRepository
    {
        private readonly ApplicationDbContext _context;

        public JobReposirtory(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Job> AddJobAsync(int personId, Job job)
        {
            var person = await _context.Persons.FindAsync(personId);
            if (person == null)
                throw new KeyNotFoundException("Person not found.");

            job.PersonId = personId;
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
            return job;
        }

        public async Task<IEnumerable<Person>> GetPersonsByCompanyAsync(string companyName)
        {
            return await _context.Persons
                .Include(p => p.Jobs)
                .Where(p => p.Jobs.Any(j => j.CompanyName == companyName))
                .ToListAsync();
        }
    }
}
