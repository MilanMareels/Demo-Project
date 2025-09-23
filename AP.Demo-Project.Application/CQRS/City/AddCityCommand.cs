using AP.Demo_Project.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Demo_Project.Application.CQRS.City
{
    public class AddCityCommand : IRequest<CityDetailDTO>
    {
        public CityDetailDTO City { get; set; }
    }

    public class AddCityCommandHandler : IRequestHandler<AddCityCommand, CityDetailDTO>
    {
        private readonly IUnitofWork uow;

        public AddCityCommandHandler(IUnitofWork uow)
        {
            this.uow = uow;
        }

        public async Task<CityDetailDTO> Handle(AddCityCommand request, CancellationToken cancellationToken)
        {
            var created = await uow.CityRepository.AddCity(request.City);
            await uow.Commit();
            return created;
        }
    }
}
