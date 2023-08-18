using Decenea.Domain.Entities.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Data.EntityConfigurations;

public class MunicipalityConfiguration : IEntityTypeConfiguration<Municipality>
{
        public void Configure(EntityTypeBuilder<Municipality> builder)
        {
                builder.ToTable(name: "Municipalities");
                
                builder.Property(i => i.AsciiName)
                        .IsRequired(false);
                
                builder.Property(i => i.AlternativeName)
                        .IsRequired(false);
                
                builder.HasOne(i => i.RegionalUnit)
                        .WithMany(i => i.Municipalities)
                        .HasForeignKey(i => i.RegionalUnitId);
                
                builder.HasMany(i => i.MunicipalUnits)
                        .WithOne(i => i.Municipality)
                        .HasForeignKey(i => i.MunicipalityId);
        }
}