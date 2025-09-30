using AP.Demo_Project.Application.CQRS.City;
using AP.Demo_Project.Application.Interfaces;
using AP.Demo_Project.Domain;
using AP.Demo_Project.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AP.Demo_Project.Infrastructure.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        private readonly DemoContext context;

        public CityRepository(DemoContext context) : base(context)
        {
            this.context = context;
        }
        
        public Task<CityDetailDTO> AddCity(CityDetailDTO city)
        {
            var normalizedName = city.Name.Trim();
            
            var entity = new City
            {
                Name = normalizedName,
                Population = city.Population,
                CountryId = city.CountryId
            };

            context.Cities.Add(entity);

            return Task.FromResult(new CityDetailDTO
            {
                Name = entity.Name,
                Population = entity.Population,
                CountryId = entity.CountryId
            });
        }
    }
}