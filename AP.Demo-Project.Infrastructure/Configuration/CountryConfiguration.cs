using AP.Demo_Project.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.Demo_Project.Infrastructure.Configuration
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("tblCountries", "Country")
                   .HasKey(c => c.Id);

            builder.HasIndex(c => c.Name)
                   .IsUnique();

            builder.Property(c => c.Id)
                   .HasColumnType("int");

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasColumnType("nvarchar(60)");

            builder.HasMany(c => c.Cities)
                   .WithOne(c => c.Country);
        }
    }
}
