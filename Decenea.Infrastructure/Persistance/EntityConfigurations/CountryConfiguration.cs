using Decenea.Domain.Aggregates.CountryAggregate;
using Decenea.Domain.Aggregates.LocationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
        public void Configure(EntityTypeBuilder<Country> builder)
        {
                builder.ToTable(name: "Countries");
                
                builder.HasIndex(i => i.Name)
                        .IsUnique();
                
                builder.Property(i => i.AsciiName)
                        .IsRequired(false);
                
                builder.Property(i => i.AlternativeName)
                        .IsRequired(false);
        }
}