using Decenea.Domain.Entities.ApplicationUserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.WebAPI.Infrastructure.Data.EntityConfigurations;

public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
                builder.ToTable(name: "Roles");
        }
}