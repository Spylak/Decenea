using Decenea.Infrastructure.Persistence.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistence.EntityConfigurations.User;

public class UserConfiguration : AuditableAggregateConfiguration<Domain.Aggregates.UserAggregate.User>
{
    public override void Configure(EntityTypeBuilder<Domain.Aggregates.UserAggregate.User> builder)
    {
        base.Configure(builder);

        builder.ToTable(name: "Users");
        
        builder.HasIndex(u => u.Email)
            .IsUnique();
    }
}