using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Demo_Project.Application.CQRS.City
{
    public class UpdateCityCommand : IRequest<CityDetailDTO>
    {
        public CityUpdateDTO City { get; set; }
    }
}
