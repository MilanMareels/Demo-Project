using AP.Demo_Project.Application.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AP.Demo_Project.Application.CQRS.City
{
    public class AddCityCommandValidator : AbstractValidator<AddCityCommand>
    {
        private readonly IUnitofWork uow;

        public AddCityCommandValidator(IUnitofWork uow) 
        {
            this.uow = uow;

            // City name validation
            RuleFor(s => s.City.Name)
                .NotNull()
                .WithMessage("Name cannot be null");

            RuleFor(s => s.City.Name)
                .NotEmpty()
                .WithMessage("Name is required");

            // Population validation
            RuleFor(s => s.City.Population)
                .GreaterThan(0)
                .WithMessage("Population must be greater than 0");

            RuleFor(s => s.City.Population)
                .LessThanOrEqualTo(10_000_000_000L)
                .WithMessage("Population must be less than or equal to 10,000,000,000");

            // CountryId validation
            RuleFor(s => s.City.CountryId)
                .GreaterThan(0)
                .WithMessage("A country must be selected");

            // Uniqueness check
            RuleFor(s => s.City.Name)
                .MustAsync(async (name, cancellation) =>
                {
                    if (string.IsNullOrWhiteSpace(name))
                        return true;

                    var existingCities = uow.CityRepository.GetAll();
                    var normalizedName = name.Trim();
                    return !existingCities.Any(c => c.Name.Equals(normalizedName, StringComparison.OrdinalIgnoreCase));
                })
                .WithMessage("A city with this name already exists");
        }
    }
}
