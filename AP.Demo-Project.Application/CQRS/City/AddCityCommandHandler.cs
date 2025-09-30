using AP.Demo_Project.Application.Interfaces;
using AP.Demo_Project.Domain;
using AutoMapper;
using MediatR;

namespace AP.Demo_Project.Application.CQRS.City
{
    public class AddCityCommandHandler : IRequestHandler<AddCityCommand, CityDetailDTO>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper mapper;

        public AddCityCommandHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<CityDetailDTO> Handle(AddCityCommand request, CancellationToken cancellationToken)
        {
            // City name check
            if (string.IsNullOrWhiteSpace(request.City.Name))
                throw new ArgumentException("Name is required.", nameof(request.City.Name));

            // Population check
            if (request.City.Population <= 0 || request.City.Population > 10_000_000_000L)
                throw new ArgumentOutOfRangeException(nameof(request.City.Population), "Population must be between 0 and 10,000,000,000.");

            // CountryId check
            if (request.City.CountryId <= 0)
                throw new ArgumentOutOfRangeException(nameof(request.City.CountryId), "A country must be selected.");

            // Uniqueness check
            var existingCities = await uow.CityRepository.GetCitiesAll();
            var normalizedName = request.City.Name.Trim();

            if (existingCities.Any(c => c.Name.Equals(normalizedName, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("A city with this name already exists.");

            var created = await uow.CityRepository.AddCity(mapper.Map<CityDetailDTO>(request.City));
            await uow.Commit();
            return created;
        }
    }
}
