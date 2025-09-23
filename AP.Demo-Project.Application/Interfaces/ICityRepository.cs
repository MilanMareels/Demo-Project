using AP.Demo_Project.Application.CQRS.City;
using AP.Demo_Project.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Demo_Project.Application.Interfaces
{
    public interface ICityRepository : IGenericRepository<City>
    {
        Task<CityDetailDTO> AddCity(CityDetailDTO city);
        Task<CityDetailDTO> UpdateCity(CityUpdateDTO city);
    }
}
