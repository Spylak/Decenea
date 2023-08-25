using Decenea.Domain.Entities.LocationEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.WebAPI.Infrastructure.Data.EntityConfigurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
        public void Configure(EntityTypeBuilder<Country> builder)
        {
                builder.ToTable(name: "Countries");
                
                builder.HasIndex(i => i.Name)
                        .IsUnique();
                
                builder.Property(i => i.AsciiName)
                        .IsRequired(false);
                
                builder.Property(i => i.AlternativeName)
                        .IsRequired(false);
                
                builder.HasMany(i => i.Cities)
                        .WithOne(i => i.Country)
                        .HasForeignKey(i => i.CountryId);
                
                builder.HasMany(i => i.Regions)
                        .WithOne(i => i.Country)
                        .HasForeignKey(i => i.CountryId);
        }
}