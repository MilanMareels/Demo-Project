using AP.Demo_Project.Application.CQRS.City;
using AP.Demo_Project.Application.CQRS.Country;
using AP.Demo_Project.Application.Interfaces;
using AP.Demo_Project.Domain;
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

        public CityService(IUnitofWork uow)
        {
            this.uow = uow;
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

            return cities.Select(c => new CityWithCountryDTO
            {
                Id = c.Id,
                Name = c.Name,
                Population = c.Population,
                CountryId = c.CountryId,
                Country = new CountryDTO
                {
                    Id = c.Country.Id,
                    Name = c.Country.Name
                }
            });
        }


        public async Task<CityDetailDTO> Add(CityDetailDTO city)
        {
            await uow.CityRepository.AddCity(city);
            await uow.Commit();
            return city;
        }
    }
}
