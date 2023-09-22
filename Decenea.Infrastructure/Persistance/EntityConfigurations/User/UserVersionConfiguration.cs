using Decenea.Domain.Aggregates.UserAggregate;
using Decenea.Infrastructure.Persistance.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations.User;

public class UserVersionConfiguration : EntityVersionTypeConfiguration<UserVersion>
{
    public override void Configure(EntityTypeBuilder<UserVersion> builder)
    {
        base.Configure(builder);
        
        builder.ToTable(name: "UserVersions");
    }
}