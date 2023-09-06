using Decenea.Domain.Aggregates.ApplicationUserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
                builder.ToTable(name: "Users");
        }
}