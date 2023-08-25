using Decenea.Domain.Entities.LocationEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.WebAPI.Infrastructure.Data.EntityConfigurations;

public class MunicipalUnitConfiguration : IEntityTypeConfiguration<MunicipalUnit>
{
        public void Configure(EntityTypeBuilder<MunicipalUnit> builder)
        {
                builder.ToTable(name: "MunicipalUnits");
                
                builder.Property(i => i.AsciiName)
                        .IsRequired(false);
                
                builder.Property(i => i.AlternativeName)
                        .IsRequired(false);
                
                builder.HasOne(i => i.Municipality)
                        .WithMany(i => i.MunicipalUnits)
                        .HasForeignKey(i => i.MunicipalityId);

                builder.HasMany(i => i.Communities)
                        .WithOne(i => i.MunicipalUnit)
                        .HasForeignKey(i => i.MunicipalUnitId);
        }
}