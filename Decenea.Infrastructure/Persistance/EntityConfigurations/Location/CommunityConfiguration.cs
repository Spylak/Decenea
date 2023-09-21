using Decenea.Domain.Aggregates.LocationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations.Location;

public class CommunityConfiguration : IEntityTypeConfiguration<Community>
{
        public void Configure(EntityTypeBuilder<Community> builder)
        {
                builder.ToTable(name: "Communities");
                
                builder.Property(p => p.Id)
                        .HasMaxLength(26);

                builder.Property(i => i.AsciiName)
                        .IsRequired(false);
                
                builder.Property(i => i.AlternativeName)
                        .IsRequired(false);
        }
}