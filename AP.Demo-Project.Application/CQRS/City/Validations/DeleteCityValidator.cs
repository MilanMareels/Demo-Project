using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Demo_Project.Application.CQRS.City.Validations
{
    public class DeleteCityValidator
    {
        public void Validate(IEnumerable<Domain.City> cities, int id)
        {
            if (!cities.Any())
                throw new InvalidOperationException("No cities found.");

            if (cities.Count() == 1)
                throw new InvalidOperationException("The last city cannot be deleted.");

            if (!cities.Any(c => c.Id == id))
                throw new KeyNotFoundException($"City with id {id} not found.");
        }
    }
}
