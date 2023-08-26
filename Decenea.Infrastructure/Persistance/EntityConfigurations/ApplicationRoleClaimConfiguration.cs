using Decenea.Domain.Entities.ApplicationUserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations;

public class ApplicationRoleClaimConfiguration : IEntityTypeConfiguration<ApplicationRoleClaim>
{
        public void Configure(EntityTypeBuilder<ApplicationRoleClaim> builder)
        {
                builder.ToTable(name: "RoleClaims");
                
                builder.Property(p => p.Id)
                        .HasColumnType("bigint");
                // Setup relationship to ApplicationRole
                builder.HasOne(ur => ur.Role)
                        .WithMany(r => r.RoleClaims)
                        .HasForeignKey(ur => ur.RoleId);
        }
}