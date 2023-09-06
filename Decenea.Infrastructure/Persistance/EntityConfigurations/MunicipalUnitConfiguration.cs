using Decenea.Domain.Aggregates.CityAggregate;
using Decenea.Domain.Aggregates.LocationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations;

public class MunicipalUnitConfiguration : IEntityTypeConfiguration<MunicipalUnit>
{
        public void Configure(EntityTypeBuilder<MunicipalUnit> builder)
        {
                builder.ToTable(name: "MunicipalUnits");
                
                builder.Property(i => i.AsciiName)
                        .IsRequired(false);
                
                builder.Property(i => i.AlternativeName)
                        .IsRequired(false);
        }
}