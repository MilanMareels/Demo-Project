using AP.Demo_Project.Application.Interfaces;
using AP.Demo_Project.Infrastructure.Contexts;
using static System.Net.Mime.MediaTypeNames;
using AP.Demo_Project.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Demo_Project.Infrastructure.UoW
{
    public class UnitofWork : IUnitofWork
    {
        private readonly DemoContext context;
        private readonly ICityRepository cityRepository;

        public UnitofWork(DemoContext context, ICityRepository cityRepository)
        {
            this.context = context;
            this.cityRepository = cityRepository;
        }
        public ICityRepository CityRepository => cityRepository;

        public async Task Commit()
        {
            await context.SaveChangesAsync();
        }
    }
}
