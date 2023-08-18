using Decenea.Domain.Entities.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Data.EntityConfigurations;

public class CommunityConfiguration : IEntityTypeConfiguration<Community>
{
        public void Configure(EntityTypeBuilder<Community> builder)
        {
                builder.ToTable(name: "Communities");
                
                builder.Property(i => i.AsciiName)
                        .IsRequired(false);
                
                builder.Property(i => i.AlternativeName)
                        .IsRequired(false);

                builder.HasOne(i => i.MunicipalUnit)
                        .WithMany(i => i.Communities)
                        .HasForeignKey(i => i.MunicipalUnitId);
                
                builder.HasMany(i => i.Cities)
                        .WithOne(i => i.Community)
                        .HasForeignKey(i => i.CommunityId);
        }
}