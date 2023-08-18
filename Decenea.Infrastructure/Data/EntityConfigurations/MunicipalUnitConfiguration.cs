using Decenea.Domain.Entities.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Data.EntityConfigurations;

public class MunicipalUnitConfiguration : IEntityTypeConfiguration<MunicipalUnit>
{
        public void Configure(EntityTypeBuilder<MunicipalUnit> builder)
        {
                builder.ToTable(name: "MunicipalUnits");
                
                builder.HasIndex(i => i.Name)
                        .IsUnique();
                
                builder.Property(i => i.AsciiName)
                        .IsRequired(false);
                
                builder.Property(i => i.AlternativeName)
                        .IsRequired(false);
                
                builder.HasOne(i => i.Municipality)
                        .WithMany(i => i.MunicipalUnits)
                        .HasForeignKey(i => i.MunicipalityId);

                builder.HasOne(i => i.Country)
                        .WithMany(i => i.MunicipalUnits)
                        .HasForeignKey(i => i.CountryId);

                builder.HasOne(i => i.Prefecture)
                        .WithMany(i => i.MunicipalUnits)
                        .HasForeignKey(i => i.PrefectureId);

                builder.HasOne(i => i.Municipality)
                        .WithMany(i => i.MunicipalUnits)
                        .HasForeignKey(i => i.MunicipalityId);

                builder.HasOne(i => i.Region)
                        .WithMany(i => i.MunicipalUnits)
                        .HasForeignKey(i => i.RegionId);

                builder.HasMany(i => i.Cities)
                        .WithOne(i => i.MunicipalUnit)
                        .HasForeignKey(i => i.MunicipalUnitId);
        }
}