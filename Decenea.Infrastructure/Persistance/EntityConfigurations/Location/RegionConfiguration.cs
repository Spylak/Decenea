using Decenea.Domain.Aggregates.LocationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations.Location;

public class RegionConfiguration : IEntityTypeConfiguration<Region>
{
        public void Configure(EntityTypeBuilder<Region> builder)
        {
                builder.ToTable(name: "Regions");
                
                builder.Property(p => p.Id)
                        .HasMaxLength(26);

                builder.Property(i => i.AsciiName)
                        .IsRequired(false);
                
                builder.Property(i => i.AlternativeName)
                        .IsRequired(false);
        }
}