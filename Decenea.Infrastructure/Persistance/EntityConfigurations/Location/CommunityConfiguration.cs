using Decenea.Domain.Aggregates.LocationAggregate;
using Decenea.Infrastructure.Persistance.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations.Location;

public class CommunityConfiguration : AuditableEntityTypeConfiguration<Community>
{
    public override void Configure(EntityTypeBuilder<Community> builder)
    {
        base.Configure(builder);

        builder.ToTable(name: "Communities");

        builder.Property(i => i.AsciiName)
            .IsRequired(false);

        builder.Property(i => i.AlternativeName)
            .IsRequired(false);
        
        builder.Property(i => i.MunicipalUnitId)
            .IsRequired();
                
        builder.HasMany(i => i.Cities)
            .WithOne()
            .HasForeignKey(i => i.CommunityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}