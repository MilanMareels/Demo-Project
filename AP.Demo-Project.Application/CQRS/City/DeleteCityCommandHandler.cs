using AP.Demo_Project.Application.CQRS.City.Validations;
using AP.Demo_Project.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Demo_Project.Application.CQRS.City
{
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
            var cities = _uow.CityRepository.GetAll();

            // VALIDATIE
            new DeleteCityValidator().Validate(cities, request.Id);

            var city = cities.First(c => c.Id == request.Id);

            _uow.CityRepository.DeleteAsync(city); 
            await _uow.Commit();             

            await _emailService.SendCityDeletedEmail(city.Name);

            return true;
        }
    }
}
