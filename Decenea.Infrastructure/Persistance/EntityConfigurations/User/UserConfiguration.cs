using Decenea.Infrastructure.Persistance.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations.User;

public class UserConfiguration : AuditableAggregateTypeConfiguration<Domain.Aggregates.UserAggregate.User>
{
    public override void Configure(EntityTypeBuilder<Domain.Aggregates.UserAggregate.User> builder)
    {
        base.Configure(builder);

        builder.ToTable(name: "Users");
    }
}