using Decenea.Domain.Aggregates.LocationAggregate;
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
        }
}