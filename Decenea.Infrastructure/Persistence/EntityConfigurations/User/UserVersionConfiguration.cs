using Decenea.Domain.Aggregates.UserAggregate;
using Decenea.Infrastructure.Persistence.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistence.EntityConfigurations.User;

public class UserVersionConfiguration : EntityVersionConfiguration<UserVersion>
{
    public override void Configure(EntityTypeBuilder<UserVersion> builder)
    {
        base.Configure(builder);
        
        builder.ToTable(name: "UserVersions");
    }
}