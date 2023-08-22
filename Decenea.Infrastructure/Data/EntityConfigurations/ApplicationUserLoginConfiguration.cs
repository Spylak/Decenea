using Decenea.Domain.Entities.ApplicationUserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Data.EntityConfigurations;

public class ApplicationUserLoginConfiguration : IEntityTypeConfiguration<ApplicationUserLogin>
{
        public void Configure(EntityTypeBuilder<ApplicationUserLogin> builder)
        {
                builder.ToTable(name: "UserLogins");
                
                // Setup relationship to ApplicationUser
                builder.HasOne(ur => ur.User)
                        .WithMany(u => u.UserLogins)
                        .HasForeignKey(ur => ur.UserId);
        }
}