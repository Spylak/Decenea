using Decenea.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations.Common;

public class AuditableEntityTypeConfiguration<TEntity> : BaseEntityTypeConfiguration<TEntity> 
    where TEntity : AuditableEntity
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);
        
        builder.Property(p => p.CreatedBy)
            .HasMaxLength(50);
        
        builder.Property(p => p.LastModifiedBy)
            .HasMaxLength(50);
    }
}