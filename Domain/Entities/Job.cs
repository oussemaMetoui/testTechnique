using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Job
    {
        public int Id { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string JobTitle { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int PersonId { get; set; }
        [JsonIgnore]
        public Person? Person { get; set; }

    }
}
