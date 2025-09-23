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
            var updated = await uow.CityRepository.UpdateCity(request.City);
            await uow.Commit();
            return updated;
        }
    }
}
