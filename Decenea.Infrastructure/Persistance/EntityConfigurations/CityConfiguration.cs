using Decenea.Domain.Aggregates.CityAggregate;
using Decenea.Domain.Aggregates.LocationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
        public void Configure(EntityTypeBuilder<City> builder)
        {
                builder.ToTable(name: "Cities");
                
                builder.Property(i => i.AsciiName)
                        .IsRequired(false);
                
                builder.Property(i => i.AlternativeName)
                        .IsRequired(false);
        }
}