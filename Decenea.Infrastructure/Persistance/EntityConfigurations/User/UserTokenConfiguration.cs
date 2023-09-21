using Decenea.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations.User;

public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.ToTable(name: "UserClaims");
                
        builder.Property(p => p.Id)
            .HasMaxLength(26);
    }
}