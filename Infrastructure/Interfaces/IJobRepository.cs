using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface IJobRepository
    {
        Task<Job> AddJobAsync(int personId, Job job);
        Task<IEnumerable<Person>> GetPersonsByCompanyAsync(string companyName);
    }
}
