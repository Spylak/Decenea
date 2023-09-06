using Decenea.Domain.Aggregates.CityAggregate;
using Decenea.Domain.Aggregates.LocationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations;

public class MunicipalityConfiguration : IEntityTypeConfiguration<Municipality>
{
        public void Configure(EntityTypeBuilder<Municipality> builder)
        {
                builder.ToTable(name: "Municipalities");
                
                builder.Property(i => i.AsciiName)
                        .IsRequired(false);
                
                builder.Property(i => i.AlternativeName)
                        .IsRequired(false);
        }
}