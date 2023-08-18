using Decenea.Domain.Entities.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Data.EntityConfigurations;

public class PrefectureConfiguration : IEntityTypeConfiguration<Prefecture>
{
        public void Configure(EntityTypeBuilder<Prefecture> builder)
        {
                builder.ToTable(name: "Prefectures");

                builder.HasIndex(i => i.Name)
                        .IsUnique();
                
                builder.Property(i => i.AsciiName)
                        .IsRequired(false);
                
                builder.Property(i => i.AlternativeName)
                        .IsRequired(false);
                
                builder.HasOne(i => i.Country)
                        .WithMany(i => i.Prefectures)
                        .HasForeignKey(i => i.CountryId);
                
                builder.HasOne(i => i.Region)
                        .WithMany(i => i.Prefectures)
                        .HasForeignKey(i => i.RegionId);
                
                builder.HasMany(i => i.Municipalities)
                        .WithOne(i => i.Prefecture)
                        .HasForeignKey(i => i.PrefectureId);
                
                builder.HasMany(i => i.Cities)
                        .WithOne(i => i.Prefecture)
                        .HasForeignKey(i => i.PrefectureId);
        }
}