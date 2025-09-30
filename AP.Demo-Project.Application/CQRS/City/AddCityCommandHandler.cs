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
            var created = await uow.CityRepository.AddCity(mapper.Map<CityDetailDTO>(request.City));
            await uow.Commit();
            return created;
        }
    }
}
