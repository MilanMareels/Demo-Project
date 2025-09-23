using AP.Demo_Project.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Demo_Project.Infrastructure.Seeding
{
    public static class CountrySeeding
    {
        public static void Seed(this EntityTypeBuilder<Country> modelBuilder)
        {
            modelBuilder.HasData(
                new Country { Id = 1, Name = "United States" },
                new Country { Id = 2, Name = "United Kingdom" },
                new Country { Id = 3, Name = "Belgium" },
                new Country { Id = 4, Name = "France" },
                new Country { Id = 5, Name = "Netherlands" }
            );
        }
    }
}
