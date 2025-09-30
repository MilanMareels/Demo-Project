using AP.Demo_Project.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Demo_Project.Application.CQRS.City
{
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
            var query = uow.CityRepository.GetAll();

            query = query.Include(c => c.Country);

            query = (request.SortBy?.ToLower(), request.SortOrder?.ToLower()) switch
            {
                ("population", "asc") => query.OrderBy(c => c.Population).ThenBy(c => c.Name),
                ("population", "desc") => query.OrderByDescending(c => c.Population).ThenBy(c => c.Name),
                ("name", "asc") => query.OrderBy(c => c.Name),
                ("name", "desc") => query.OrderByDescending(c => c.Name),
                _ => query.OrderByDescending(c => c.Population).ThenBy(c => c.Name) // default
            };

            query = query.Skip((request.PageNr - 1) * request.PageSize)
                         .Take(request.PageSize);

            var cities = await query.ToListAsync(cancellationToken);

            return mapper.Map<IEnumerable<CityWithCountryDTO>>(cities);
        }

    }
}
