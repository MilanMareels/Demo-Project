using AP.Demo_Project.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Demo_Project.Infrastructure.Seeding
{
    public static class CitySeeding
    {
        public static void Seed(this EntityTypeBuilder<City> modelBuilder)
        {
            modelBuilder.HasData(
                new City { Id = 1, Name = "New York", Population = 8419600, CountryId = 1 },
                new City { Id = 2, Name = "Los Angeles", Population = 3980400, CountryId = 1 },
                new City { Id = 3, Name = "Chicago", Population = 2716000, CountryId = 1 },
                new City { Id = 4, Name = "Houston", Population = 2328000, CountryId = 1 },
                new City { Id = 5, Name = "Phoenix", Population = 1690000, CountryId = 1 },
                new City { Id = 6, Name = "London", Population = 8982000, CountryId = 2 },
                new City { Id = 7, Name = "Birmingham", Population = 1141000, CountryId = 2 },
                new City { Id = 8, Name = "Leeds", Population = 789000, CountryId = 2 },
                new City { Id = 9, Name = "Glasgow", Population = 635000, CountryId = 2 },
                new City { Id = 10, Name = "Sheffield", Population = 584000, CountryId = 2 }
            );
        }
    }
}
