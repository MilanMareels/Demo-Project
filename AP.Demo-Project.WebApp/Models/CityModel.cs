using System.ComponentModel.DataAnnotations;

namespace AP.Demo_Project.WebApp.Models
{
public class CityModel: IValidatableObject
    {
        public string? Name { get; set; }
        public long Population { get; set; }
        public int? CountryId { get; set; }

        // Alle validatielogica.
        // Blazor's EditForm roept deze automatisch aan.
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // 1. Validatie voor de naam
            if (string.IsNullOrWhiteSpace(Name))
            {
                yield return new ValidationResult(
                    "De naam mag niet leeg zijn.",
                    new[] { nameof(Name) });
            }

            // 2. Validatie voor het aantal inwoners
            if (Population > 10000000000 || Population < 1)
            {
                yield return new ValidationResult(
                    "Het aantal inwoners mag niet groter zijn dan 10 miljard. Of kleiner dan 1 zijn.",
                    new[] { nameof(Population) });
            }

            // 3. Validatie voor het land
            if (CountryId == null)
            {
                yield return new ValidationResult(
                    "Er moet een land gekozen worden.",
                    new[] { nameof(CountryId) });
            }
        }
    }
}
