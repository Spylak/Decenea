using Decenea.Domain.Aggregates.ApplicationUserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations;

public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
                builder.ToTable(name: "Roles");
        }
}