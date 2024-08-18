using Decenea.Infrastructure.Persistence.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistence.EntityConfigurations.Group;

public class GroupConfiguration : AuditableAggregateConfiguration<Domain.Aggregates.GroupAggregate.Group>
{
        public override void Configure(EntityTypeBuilder<Domain.Aggregates.GroupAggregate.Group> builder)
        {
                base.Configure(builder);
                builder.ToTable(name: "Groups");
        }
}