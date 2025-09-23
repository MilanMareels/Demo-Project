using AP.Demo_Project.Application.CQRS.City;
using AP.Demo_Project.Application.Interfaces;
using AP.Demo_Project.Domain;
using AP.Demo_Project.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AP.Demo_Project.Infrastructure.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        private readonly DemoContext context;

        public CityRepository(DemoContext context) : base(context)
        {
            this.context = context;
        }

        public Task<CityDetailDTO> AddCity(CityDetailDTO city)
        {
            // Server-side validation (same rules as client)
            if (string.IsNullOrWhiteSpace(city.Name))
                throw new ArgumentException("Name is required.", nameof(city.Name));

            if (city.Population <= 0 || city.Population > 10_000_000_000L)
                throw new ArgumentOutOfRangeException(nameof(city.Population), "Population must be between 0 and 10,000,000,000.");

            if (city.CountryId <= 0)
                throw new ArgumentOutOfRangeException(nameof(city.CountryId), "A country must be selected.");

            // Uniqueness check (pre-check)
            var normalizedName = city.Name.Trim();
            if (context.Cities.Any(c => c.Name == normalizedName))
                throw new InvalidOperationException("A city with this name already exists.");

            var entity = new City
            {
                Name = normalizedName,
                Population = city.Population,
                CountryId = city.CountryId
            };

            context.Cities.Add(entity);

            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("A city with this name already exists.", ex);
            }

            return Task.FromResult(new CityDetailDTO
            {
                Name = entity.Name,
                Population = entity.Population,
                CountryId = entity.CountryId
            });
        }

        public async Task<CityDetailDTO> UpdateCity(CityUpdateDTO city)
        {
            var entity = await context.Cities.FindAsync(city.Id);

            if (entity == null)
                throw new KeyNotFoundException("City not found.");

            // Server-side validation
            if (string.IsNullOrWhiteSpace(city.Name))
                throw new ArgumentException("Name is required.", nameof(city.Name));

            if (city.Population <= 0 || city.Population > 10_000_000_000L)
                throw new ArgumentOutOfRangeException(nameof(city.Population), "Population must be between 0 and 10,000,000,000.");

            if (city.CountryId <= 0)
                throw new ArgumentOutOfRangeException(nameof(city.CountryId), "A country must be selected.");

            // Uniqueness check
            var normalizedName = city.Name.Trim();
            if (context.Cities.Any(c => c.Name == normalizedName && c.Id != city.Id))
                throw new InvalidOperationException("A city with this name already exists.");

            entity.Name = normalizedName;
            entity.Population = city.Population;
            entity.CountryId = city.CountryId;

            context.Cities.Update(entity);
            await context.SaveChangesAsync();

            return new CityDetailDTO
            {
                Name = entity.Name,
                Population = entity.Population,
                CountryId = entity.CountryId
            };
        }
    }
}
