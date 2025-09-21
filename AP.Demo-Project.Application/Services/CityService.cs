using AP.Demo_Project.Application.CQRS.City;
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

        public async Task<IEnumerable<City>> GetAll(int pageNr, int pageSize)
        {
            return await uow.CityRepository.GetAll(pageNr,pageSize);
        }

        public async Task<CityDetailDTO> Add(CityDetailDTO city)
        {
            await uow.CityRepository.AddCity(city);
            await uow.Commit();
            return city;
        }
    }
}
