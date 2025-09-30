using MediatR;

namespace AP.Demo_Project.Application.CQRS.City
{
    public class AddCityCommand : IRequest<CityDetailDTO>
    {
        public CityDetailDTO City { get; set; }
    }
}
