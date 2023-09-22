using Decenea.Domain.Aggregates.LocationAggregate;
using Decenea.Infrastructure.Persistance.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations.Location;

public class MunicipalUnitConfiguration : AuditableEntityTypeConfiguration<MunicipalUnit>
{
        public override void Configure(EntityTypeBuilder<MunicipalUnit> builder)
        {
                base.Configure(builder);

                builder.ToTable(name: "MunicipalUnits");

                builder.Property(i => i.AsciiName)
                        .IsRequired(false);
                
                builder.Property(i => i.AlternativeName)
                        .IsRequired(false);
        }
}