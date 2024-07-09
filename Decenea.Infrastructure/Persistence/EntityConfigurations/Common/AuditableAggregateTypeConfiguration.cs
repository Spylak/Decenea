using Decenea.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistence.EntityConfigurations.Common;

public class AuditableAggregateTypeConfiguration<TEntity> : AuditableEntityTypeConfiguration<TEntity> 
    where TEntity : AuditableAggregateRoot
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);
    }
}