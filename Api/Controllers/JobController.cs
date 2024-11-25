using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAtrioBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpPost("{personId}")]
        public async Task<IActionResult> AddJob(int personId, Job job)
        {
            try
            {
                job.PersonId = personId; 
                var createdJob = await _jobService.AddJobAsync(personId, job);
                return CreatedAtAction(nameof(AddJob), new { id = createdJob.Id }, createdJob);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("company/{companyName}")]
        public async Task<IActionResult> GetPersonsByCompany(string companyName)
        {
            var persons = await _jobService.GetPersonsByCompanyAsync(companyName);
            return Ok(persons);
        }
    }
}
