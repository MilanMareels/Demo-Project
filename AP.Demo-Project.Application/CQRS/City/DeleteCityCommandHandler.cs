using AP.Demo_Project.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Demo_Project.Application.CQRS.City
{
    /*
    public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand, bool>
    {
        private readonly IUnitofWork _uow;
        private readonly IEmailService _emailService;

        public DeleteCityCommandHandler(IUnitofWork uow, IEmailService emailService)
        {
            _uow = uow;
            _emailService = emailService;
        }

        
        public async Task<bool> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            var cities =  _uow.CityRepository.GetAll();

            if (!cities.Any())
                throw new InvalidOperationException("No cities found.");

            if (cities.Count() == 1)
                throw new InvalidOperationException("The last city cannot be deleted.");

            var city = cities.FirstOrDefault(c => c.Id == request.Id);
            if (city == null)
                throw new KeyNotFoundException($"City with id {request.Id} not found.");

            await _uow.CityRepository.DeleteAsync(city);

            await _uow.Commit();

            await _emailService.SendCityDeletedEmail(city.Name);

            return true;
        } 
    }
    */
}
