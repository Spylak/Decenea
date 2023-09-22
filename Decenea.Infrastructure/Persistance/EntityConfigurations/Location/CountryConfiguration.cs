using Decenea.Domain.Aggregates.LocationAggregate;
using Decenea.Infrastructure.Persistance.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations.Location;

public class CountryConfiguration : AuditableAggregateTypeConfiguration<Country>
{
    public override void Configure(EntityTypeBuilder<Country> builder)
    {
        base.Configure(builder);

        builder.ToTable(name: "Countries");

        builder.HasIndex(i => i.Name)
            .IsUnique();

        builder.Property(i => i.AsciiName)
            .IsRequired(false);

        builder.Property(i => i.AlternativeName)
            .IsRequired(false);
    }
}