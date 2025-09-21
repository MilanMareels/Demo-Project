using AP.Demo_Project.Application.CQRS.City;
using AP.Demo_Project.Domain;
using System;

namespace AP.Demo_Project.Application.Interfaces
{
    public interface ICityService
    {
        public Task<IEnumerable<CityWithCountryDTO>> GetAll(int pageNr, int pageSize, string sortBy, string sortOrder);
        public Task<CityDetailDTO> Add(CityDetailDTO city);
    }
}
