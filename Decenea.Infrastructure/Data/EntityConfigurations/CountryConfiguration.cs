using Decenea.Domain.Entities.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Data.EntityConfigurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
        public void Configure(EntityTypeBuilder<Country> builder)
        {
                builder.ToTable(name: "Countries");
                
                builder.HasIndex(i => i.Name)
                        .IsUnique();
                
                builder.HasMany(i => i.Prefectures)
                        .WithOne(i => i.Country)
                        .HasForeignKey(i => i.CountryId);
                
                builder.HasMany(i => i.Municipalities)
                        .WithOne(i => i.Country)
                        .HasForeignKey(i => i.CountryId);
                
                builder.HasMany(i => i.Cities)
                        .WithOne(i => i.Country)
                        .HasForeignKey(i => i.CountryId);
                
                builder.HasMany(i => i.Regions)
                        .WithOne(i => i.Country)
                        .HasForeignKey(i => i.CountryId);
                
                builder.HasMany(i => i.MunicipalUnits)
                        .WithOne(i => i.Country)
                        .HasForeignKey(i => i.CountryId);
        }
}