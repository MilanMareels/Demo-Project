using System.ComponentModel.DataAnnotations;

namespace AP.Demo_Project.WebApp.DTO
{
    public class CityUpdateDTO
{
        [Required]
        public int Id { get; set; }

        [Range(1, 10_000_000_000, ErrorMessage = "Population must be between 1 and 10,000,000,000.")]
        public long Population { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "A country must be selected.")]
        public int CountryId { get; set; }
    }
}
