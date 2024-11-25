using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Interfaces
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAllPersonsAsync();
        Task<Person> CreatePersonAsync(Person person);
        Task<IEnumerable<Job>> GetJobsByPersonIdAsync(int personId);
        Task<IEnumerable<Job>> GetJobsBetweenDatesAsync(int personId, DateTime startDate, DateTime endDate);
        Task<Person> GetByIdAsync(int id);
    }
}
