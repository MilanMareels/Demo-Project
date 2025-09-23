using AP.Demo_Project.Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Demo_Project.Application.CQRS.City
{
    public class GetByIdCommand : IRequest<CityWithCountryDTO>
    {
        public int Id { get; set; }
    }

    public class GetByIdCommandHandler : IRequestHandler<GetByIdCommand, CityWithCountryDTO>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper mapper;

        public GetByIdCommandHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        public async Task<CityWithCountryDTO> Handle(GetByIdCommand request, CancellationToken cancellationToken)
        {
            return mapper.Map<CityWithCountryDTO>(await uow.CityRepository.GetByIdAsync(request.Id, c => c.Country));
        }
    }
}
