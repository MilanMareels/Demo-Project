using AP.Demo_Project.Application.Interfaces;
using AP.Demo_Project.Infrastructure.Contexts;
using AP.Demo_Project.Infrastructure.Repositories;
using AP.Demo_Project.Infrastructure.Services;
using AP.Demo_Project.Infrastructure.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AP.Demo_Project.Infrastructure.Extentions
{
    public static class Registrator
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
        {
            services.RegisterDbContext();
            services.RegisterRepositories();

            // Email service
            services.AddScoped<IEmailService, FakeEmailService>();

            return services;
        }
        public static IServiceCollection RegisterDbContext(this IServiceCollection services)
        {
            services.AddDbContext<DemoContext>(options =>
                        options.UseSqlServer("name=ConnectionStrings:DemoProject"));

            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IUnitofWork, UnitofWork>();
            return services;
        }
    }
}
