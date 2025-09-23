using System.ComponentModel.DataAnnotations;

namespace AP.Demo_Project.WebApp.DTO
{
    public class CityCreateDTO
    {
        [Required(ErrorMessage = "Naam is verplicht.")]
        public string Name { get; set; }

        [Range(1, 10000000000, ErrorMessage = "Aantal inwoners moet tussen 1 en 10.000.000.000 liggen.")]
        public long Population { get; set; }

        [Required(ErrorMessage = "Kies een land.")]
        public int CountryId { get; set; }
    }
}
