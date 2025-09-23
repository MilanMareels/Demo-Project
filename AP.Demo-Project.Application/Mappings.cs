using AP.Demo_Project.Application.CQRS.City;
using AP.Demo_Project.Application.CQRS.Country;
using AP.Demo_Project.Domain;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Demo_Project.Application
{
    internal class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<City, CityWithCountryDTO>();
            CreateMap<Country, CountryDTO>();
        }
    }
}
