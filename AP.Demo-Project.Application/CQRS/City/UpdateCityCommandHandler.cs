using AP.Demo_Project.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Demo_Project.Application.CQRS.City
{
    public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand, CityDetailDTO>
    {
        private readonly IUnitofWork uow;

        public UpdateCityCommandHandler(IUnitofWork uow)
        {
            this.uow = uow;
        }

        public async Task<CityDetailDTO> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            // Get the existing city entity
            var existingCity = await uow.CityRepository.GetByIdAsync(request.City.Id);
            
            if (existingCity == null)
                throw new KeyNotFoundException("City not found.");

            // Update the entity properties
            existingCity.Population = request.City.Population;
            existingCity.CountryId = request.City.CountryId;

            // Use the generic Update method
            uow.CityRepository.Update(existingCity);
            await uow.Commit();

            // Return the updated data as DTO
            return new CityDetailDTO
            {
                Name = existingCity.Name,
                Population = existingCity.Population,
                CountryId = existingCity.CountryId
            };
        }
    }
}
