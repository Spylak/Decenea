using Decenea.Domain.Entities.LocationEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Data.EntityConfigurations;

public class RegionConfiguration : IEntityTypeConfiguration<Region>
{
        public void Configure(EntityTypeBuilder<Region> builder)
        {
                builder.ToTable(name: "Regions");
                
                builder.Property(i => i.AsciiName)
                        .IsRequired(false);
                
                builder.Property(i => i.AlternativeName)
                        .IsRequired(false);
                
                builder.HasOne(i => i.Country)
                        .WithMany(i => i.Regions)
                        .HasForeignKey(i => i.CountryId);
                
                builder.HasMany(i => i.RegionalUnits)
                        .WithOne(i => i.Region)
                        .HasForeignKey(i => i.RegionId);
        }
}