using Decenea.Domain.Entities.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Data.EntityConfigurations;

public class MunicipalityConfiguration : IEntityTypeConfiguration<Municipality>
{
        public void Configure(EntityTypeBuilder<Municipality> builder)
        {
                builder.ToTable(name: "Municipalities");

                builder.HasIndex(i => i.Name)
                        .IsUnique();
                
                builder.Property(i => i.AsciiName)
                        .IsRequired(false);
                
                builder.Property(i => i.AlternativeName)
                        .IsRequired(false);
                
                builder.HasOne(i => i.Country)
                        .WithMany(i => i.Municipalities)
                        .HasForeignKey(i => i.CountryId);
                
                builder.HasOne(i => i.Seat)
                        .WithOne()
                        .HasForeignKey<Municipality>(i => i.SeatId)
                        .IsRequired(false);
                
                builder.HasOne(i => i.Region)
                        .WithMany(i => i.Municipalities)
                        .HasForeignKey(i => i.RegionId);
                
                builder.HasOne(i => i.Prefecture)
                        .WithMany(i => i.Municipalities)
                        .HasForeignKey(i => i.PrefectureId);
                
                builder.HasMany(i => i.Cities)
                        .WithOne(i => i.Municipality)
                        .HasForeignKey(i => i.MunicipalityId);
                
                builder.HasMany(i => i.MunicipalUnits)
                        .WithOne(i => i.Municipality)
                        .HasForeignKey(i => i.MunicipalityId);
        }
}