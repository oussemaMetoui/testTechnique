using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetAllPersonsAsync();
        Task<Person> CreatePersonAsync(Person person);
        Task<IEnumerable<Job>> GetJobsByPersonIdAsync(int personId);
        Task<Person> GetPersonByIdAsync(int id);
        Task<IEnumerable<Job>> GetJobsBetweenDatesAsync(int personId, DateTime startDate, DateTime endDate);
    }
}
