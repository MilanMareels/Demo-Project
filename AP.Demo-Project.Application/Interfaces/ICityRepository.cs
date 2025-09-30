using AP.Demo_Project.Application.CQRS.City;
using AP.Demo_Project.Domain;

namespace AP.Demo_Project.Application.Interfaces
{
    public interface ICityRepository : IGenericRepository<City>
    {
        Task<CityDetailDTO> AddCity(CityDetailDTO city);
        Task DeleteAsync(City city);
    }
}
