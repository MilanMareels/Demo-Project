using AP.Demo_Project.Application.CQRS.City;
using AP.Demo_Project.Application.CQRS.Country;
using AP.Demo_Project.Application.Interfaces;
using AP.Demo_Project.Domain;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Demo_Project.Application.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitofWork uow;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CityService(IUnitofWork uow, IMapper mapper, IMediator mediator)
        {
            this.uow = uow;
            this._mapper = mapper;
            this._mediator = mediator;
        }

        public async Task<IEnumerable<CityWithCountryDTO>> GetAll(int pageNr, int pageSize, string sortBy, string sortOrder)
        {
            var cities = await uow.CityRepository.GetAll(pageNr, pageSize, c => c.Country);

            // Sorting
            cities = (sortBy.ToLower(), sortOrder.ToLower()) switch
            {
                ("population", "asc") => cities.OrderBy(c => c.Population),
                ("population", "desc") => cities.OrderByDescending(c => c.Population),
                ("name", "asc") => cities.OrderBy(c => c.Name),
                ("name", "desc") => cities.OrderByDescending(c => c.Name),
                _ => cities.OrderBy(c => c.Id)
            };

            return _mapper.Map<IEnumerable<CityWithCountryDTO>>(cities);
        }

        public async Task<CityDetailDTO> Add(CityDetailDTO city)
        {
            return await _mediator.Send(new AddCityCommand { City = city });
        }
    }
}
