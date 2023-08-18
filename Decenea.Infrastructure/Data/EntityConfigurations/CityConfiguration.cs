using Decenea.Domain.Entities.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Data.EntityConfigurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
        public void Configure(EntityTypeBuilder<City> builder)
        {
                builder.ToTable(name: "Cities");
                
                builder.HasIndex(i => i.Name)
                        .IsUnique();
                
                builder.Property(i => i.AsciiName)
                        .IsRequired(false);
                
                builder.Property(i => i.AlternativeName)
                        .IsRequired(false);

                builder.HasOne(i => i.Country)
                        .WithMany(i => i.Cities)
                        .HasForeignKey(i => i.CountryId);

                builder.HasOne(i => i.Prefecture)
                        .WithMany(i => i.Cities)
                        .HasForeignKey(i => i.PrefectureId);

                builder.HasOne(i => i.Municipality)
                        .WithMany(i => i.Cities)
                        .HasForeignKey(i => i.MunicipalityId);

                builder.HasOne(i => i.Region)
                        .WithMany(i => i.Cities)
                        .HasForeignKey(i => i.RegionId);

                builder.HasOne(i => i.MunicipalUnit)
                        .WithMany(i => i.Cities)
                        .HasForeignKey(i => i.MunicipalityId);
        }
}