using Decenea.Domain.Aggregates.LocationAggregate;
using Decenea.Infrastructure.Persistance.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations.Location;

public class CityConfiguration : AuditableEntityTypeConfiguration<City>
{
        public override void Configure(EntityTypeBuilder<City> builder)
        {
                base.Configure(builder);
                
                builder.ToTable(name: "Cities");
                
                builder.Property(i => i.AsciiName)
                        .IsRequired(false);
                
                builder.Property(i => i.AlternativeName)
                        .IsRequired(false);
                
                builder.Property(i => i.CommunityId)
                        .IsRequired(false);
                
                builder.Property(i => i.MunicipalUnitId)
                        .IsRequired(false);
                
                builder.Property(i => i.MunicipalityId)
                        .IsRequired(false);
                
                builder.Property(i => i.RegionalUnitId)
                        .IsRequired(false);
                
                builder.Property(i => i.RegionId)
                        .IsRequired(false);
                
                builder.Property(i => i.CountryId)
                        .IsRequired();
        }
}