using Decenea.Domain.Aggregates.UserAggregate;
using Decenea.Infrastructure.Persistence.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistence.EntityConfigurations.User;

public class UserTokenConfiguration : AuditableEntityTypeConfiguration<UserToken>
{
    public override void Configure(EntityTypeBuilder<UserToken> builder)
    {
        base.Configure(builder);

        builder.ToTable(name: "UserTokens");
    }
}