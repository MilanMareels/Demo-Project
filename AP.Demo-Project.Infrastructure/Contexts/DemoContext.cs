using System.Reflection;
using Microsoft.EntityFrameworkCore;
using AP.Demo_Project.Domain;
using AP.Demo_Project.Infrastructure.Seeding;

namespace AP.Demo_Project.Infrastructure.Contexts
{
    public class DemoContext : DbContext
    {
        public DemoContext(DbContextOptions<DemoContext> options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Country>().Seed();
            modelBuilder.Entity<City>().Seed();
        }
    }
}
