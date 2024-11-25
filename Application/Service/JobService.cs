using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class JobService : IJobService
    {
        private IJobRepository _jobRepository { get; set; }
        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public Task<Job> AddJobAsync(int personId, Job job)
        {
            return _jobRepository.AddJobAsync(personId, job);
        }

        public Task<IEnumerable<Person>> GetPersonsByCompanyAsync(string companyName)
        {
            return _jobRepository.GetPersonsByCompanyAsync(companyName);
        }
    }
}
