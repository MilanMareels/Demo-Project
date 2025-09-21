using AP.Demo_Project.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Demo_Project.Application.CQRS.City
{
    public class CityDetailDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name cannot exceed 60 characters")]
        public string Name { get; set; } = string.Empty;

        [Range(typeof(long), "0", "10000000000", ErrorMessage = "Population must be between 0 and 10,000,000,000")]
        public long Population { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "A country must be selected")]
        public int CountryId { get; set; }
    }
}
