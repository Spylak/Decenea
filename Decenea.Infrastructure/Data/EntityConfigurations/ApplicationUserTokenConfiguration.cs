using Decenea.Domain.Entities.ApplicationUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Data.EntityConfigurations;

public class ApplicationUserTokenConfiguration : IEntityTypeConfiguration<ApplicationUserToken>
{
        public void Configure(EntityTypeBuilder<ApplicationUserToken> builder)
        {
                builder.ToTable(name: "UserTokens");
                
                // Setup relationship to ApplicationUser
                builder.HasOne(ur => ur.User)
                        .WithMany(u => u.UserTokens)
                        .HasForeignKey(ur => ur.UserId);
        }
}