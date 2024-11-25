using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAtrioBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPersons()
        {
            var persons = await _personService.GetAllPersonsAsync();
            var result = persons.Select(p => new
            {
                p.Id,
                p.FirstName,
                p.LastName,
                Age = DateTime.Now.Year - p.DateOfBirth.Year,
                CurrentJobs = p.Jobs.Where(j => j.EndDate == null).ToList()
            });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson(Person person)
        {
            try
            {
                // Validate person age
                var age = DateTime.Now.Year - person.DateOfBirth.Year;
                if (age > 150)
                {
                    return BadRequest("Person must be less than 150 years old.");
                }

                var existingJobs = await _personService.GetJobsByPersonIdAsync(person.Id);

                foreach (var job in person.Jobs)
                {
                    // Assigner PersonId à chaque emploi
                    job.PersonId = person.Id;

                    // Vérifier si les dates chevauchent d'autres emplois existants
                    if (job.EndDate == null) // Poste actuellement occupé
                    {
                        if (existingJobs.Any(ej => ej.EndDate == null || ej.EndDate > job.StartDate))
                        {
                            return BadRequest("An overlapping job already exists for this person.");
                        }
                    }
                    else
                    {
                        if (existingJobs.Any(ej =>
                            (ej.EndDate == null || ej.EndDate > job.StartDate) &&
                            (job.EndDate > ej.StartDate)))
                        {
                            return BadRequest("Overlapping job dates detected.");
                        }
                    }
                }

                var createdPerson = await _personService.CreatePersonAsync(person);
                return CreatedAtAction(nameof(GetPersons), new { id = createdPerson.Id }, createdPerson);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonById(int id)
        {
            try
            {
                var person = await _personService.GetPersonByIdAsync(id);

                if (person == null)
                {
                    return NotFound($"Person with ID {id} not found.");
                }

                var result = new
                {
                    person.Id,
                    person.FirstName,
                    person.LastName,
                    Age = DateTime.Now.Year - person.DateOfBirth.Year,
                    Jobs = person.Jobs
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving person: {ex.Message}");
            }
        }

        [HttpGet("{id}/jobs")]
        public async Task<IActionResult> GetJobsBetweenDates(int id, DateTime startDate, DateTime endDate)
        {
            try
            {
                var jobs = await _personService.GetJobsBetweenDatesAsync(id, startDate, endDate);
                return Ok(jobs);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
