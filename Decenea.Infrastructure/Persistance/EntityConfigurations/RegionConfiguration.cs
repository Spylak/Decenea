using Decenea.Domain.Aggregates.CityAggregate;
using Decenea.Domain.Aggregates.LocationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations;

public class RegionConfiguration : IEntityTypeConfiguration<Region>
{
        public void Configure(EntityTypeBuilder<Region> builder)
        {
                builder.ToTable(name: "Regions");
                
                builder.Property(i => i.AsciiName)
                        .IsRequired(false);
                
                builder.Property(i => i.AlternativeName)
                        .IsRequired(false);
        }
}