using Decenea.Domain.Entities.ApplicationUserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations;

public class ApplicationUserClaimConfiguration : IEntityTypeConfiguration<ApplicationUserClaim>
{
        public void Configure(EntityTypeBuilder<ApplicationUserClaim> builder)
        {
                builder.ToTable(name: "UserClaims");
                
                builder.Property(p => p.Id)
                        .HasColumnType("bigint");
                // Setup relationship to ApplicationUser
                builder.HasOne(ur => ur.User)
                        .WithMany(u => u.UserClaims)
                        .HasForeignKey(ur => ur.UserId);
        }
}