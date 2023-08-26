using Decenea.Domain.Entities.LocationEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations;

public class RegionalUnitConfiguration : IEntityTypeConfiguration<RegionalUnit>
{
        public void Configure(EntityTypeBuilder<RegionalUnit> builder)
        {
                builder.ToTable(name: "RegionalUnits");
                
                builder.Property(i => i.AsciiName)
                        .IsRequired(false);
                
                builder.Property(i => i.AlternativeName)
                        .IsRequired(false);
                
                builder.HasOne(i => i.Region)
                        .WithMany(i => i.RegionalUnits)
                        .HasForeignKey(i => i.RegionId);
                
                builder.HasMany(i => i.Municipalities)
                        .WithOne(i => i.RegionalUnit)
                        .HasForeignKey(i => i.RegionalUnitId);
        }
}