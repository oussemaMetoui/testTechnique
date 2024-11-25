using Domain.Entities;

namespace Application.Interfaces
{
    public interface IJobService
    {
        Task<Job> AddJobAsync(int personId, Job job);
        Task<IEnumerable<Person>> GetPersonsByCompanyAsync(string companyName);
    }
}
