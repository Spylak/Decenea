using Decenea.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations.User;

public class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
{
    public void Configure(EntityTypeBuilder<UserClaim> builder)
    {
        builder.ToTable(name: "UserClaims");
                
        builder.Property(p => p.Id)
            .HasMaxLength(26);
    }
}