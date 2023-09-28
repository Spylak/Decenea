using Decenea.Domain.Aggregates.LocationAggregate;
using Decenea.Infrastructure.Persistance.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations.Location;

public class RegionConfiguration : AuditableEntityTypeConfiguration<Region>
{
        public override void Configure(EntityTypeBuilder<Region> builder)
        {
                base.Configure(builder);

                builder.ToTable(name: "Regions");

                builder.Property(i => i.AsciiName)
                        .IsRequired(false);
                
                builder.Property(i => i.AlternativeName)
                        .IsRequired(false);
                
                builder.Property(i => i.CountryId)
                        .IsRequired();
                
                builder.HasMany(i => i.RegionalUnits)
                        .WithOne()
                        .HasForeignKey(i => i.RegionId)
                        .OnDelete(DeleteBehavior.Restrict);
        }
}