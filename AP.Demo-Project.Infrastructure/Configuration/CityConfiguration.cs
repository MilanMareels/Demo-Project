using AP.Demo_Project.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Demo_Project.Infrastructure.Configuration
{
    public class CityConfiguration
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("tblCities", "City")
                   .HasKey(c => c.Id);

            builder.HasIndex(c => c.Name).IsUnique();

            builder.Property(c => c.Id).HasColumnType("int");

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasColumnType("nvarchar(60)");

            builder.Property(c => c.Population)
                   .HasColumnType("bigint");

            builder.Property(c => c.CountryId)
                   .HasColumnType("int");

            builder.HasOne(c => c.Country)
                   .WithMany(cn => cn.Cities)
                   .HasForeignKey(c => c.CountryId);
        }
    }
}
