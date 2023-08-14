using Decenea.Domain.Entities.ApplicationUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Data.EntityConfigurations;

public class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
{
        public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
        {
                builder.ToTable(name: "UserRoles");

                builder.HasKey(k => new { k.UserId, k.RoleId });
                
                // Setup relationship to ApplicationUser
                builder.HasOne(ur => ur.User)
                        .WithMany(u => u.UserRoles)
                        .HasForeignKey(ur => ur.UserId);

                // Setup relationship to ApplicationRole
                builder.HasOne(ur => ur.Role)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.RoleId);
        }
}