using AP.Demo_Project.Application.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Demo_Project.Application.CQRS.City
{
    public class UpdateCityCommand : IRequest<CityDetailDTO>
    {
        public CityUpdateDTO City { get; set; }
    }

    public class UpdateCityValidator : AbstractValidator<UpdateCityCommand>
    {
        private IUnitofWork uow;

        public UpdateCityValidator(IUnitofWork uow)
        {
            this.uow = uow;

            RuleFor(x => x.City.Id)
                .GreaterThan(0)
                .WithMessage("City ID is required.")
                .MustAsync(async (id, cancellation) => await CityExists(id))
                .WithMessage("City not found.");

            RuleFor(x => x.City.Population)
                .GreaterThan(0)
                .WithMessage("Population must be greater than 0.")
                .LessThanOrEqualTo(10_000_000_000L)
                .WithMessage("Population must be less than or equal to 10,000,000,000.");

            RuleFor(x => x.City.CountryId)
                .GreaterThan(0)
                .WithMessage("A country must be selected.");
        }

        private async Task<bool> CityExists(int cityId)
        {
            var city = await uow.CityRepository.GetByIdAsync(cityId);
            return city != null;
        }
    }
}
