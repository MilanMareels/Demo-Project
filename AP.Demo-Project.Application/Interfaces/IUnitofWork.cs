using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Demo_Project.Application.Interfaces
{
    public interface IUnitofWork
    {
        public ICityRepository CityRepository { get; }
        Task Commit();
    }
}
