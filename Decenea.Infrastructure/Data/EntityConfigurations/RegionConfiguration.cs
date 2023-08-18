using Decenea.Domain.Entities.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Data.EntityConfigurations;

public class RegionConfiguration : IEntityTypeConfiguration<Region>
{
        public void Configure(EntityTypeBuilder<Region> builder)
        {
                builder.ToTable(name: "Regions");

                builder.HasIndex(i => i.Name)
                        .IsUnique();

                builder.Property(i => i.AsciiName)
                        .IsRequired(false);
                
                builder.Property(i => i.AlternativeName)
                        .IsRequired(false);
                
                builder.HasOne(i => i.Country)
                        .WithMany(i => i.Regions)
                        .HasForeignKey(i => i.CountryId);

                builder.HasOne(i => i.Capital)
                        .WithOne()
                        .HasForeignKey<Region>(i => i.CapitalId)
                        .IsRequired(false);
                
                builder.HasMany(i => i.Cities)
                        .WithOne(i => i.Region)
                        .HasForeignKey(i => i.RegionId);
                
                builder.HasMany(i => i.Municipalities)
                        .WithOne(i => i.Region)
                        .HasForeignKey(i => i.RegionId);
                
                builder.HasMany(i => i.Prefectures)
                        .WithOne(i => i.Region)
                        .HasForeignKey(i => i.RegionId);
        }
}