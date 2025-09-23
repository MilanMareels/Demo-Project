using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AP.Demo_Project.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace AP.Demo_Project.Application.CQRS.City
{
    public class GetAllCitiesQuery : IRequest<IEnumerable<CityWithCountryDTO>> {
        public int PageNr { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
    }

    public class GetAllCitiesQueryHandler : IRequestHandler<GetAllCitiesQuery, IEnumerable<CityWithCountryDTO>>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper mapper;

        public GetAllCitiesQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<CityWithCountryDTO>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<IEnumerable<CityWithCountryDTO>>(await uow.CityRepository.GetAll(request.PageNr, request.PageSize, request.SortBy, request.SortOrder,c => c.Country));
        }
    }
}
