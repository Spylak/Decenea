using Decenea.Domain.Aggregates.LocationAggregate;
using Decenea.Infrastructure.Persistance.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations.Location;

public class RegionalUnitConfiguration : AuditableEntityTypeConfiguration<RegionalUnit>
{
        public override void Configure(EntityTypeBuilder<RegionalUnit> builder)
        {
                base.Configure(builder);

                builder.ToTable(name: "RegionalUnits");
                
                builder.Property(i => i.AsciiName)
                        .IsRequired(false);
                
                builder.Property(i => i.AlternativeName)
                        .IsRequired(false);
                
                builder.Property(i => i.RegionId)
                        .IsRequired();
                
                builder.HasMany(i => i.Municipalities)
                        .WithOne()
                        .HasForeignKey(i => i.RegionalUnitId)
                        .OnDelete(DeleteBehavior.Restrict);
        }
}