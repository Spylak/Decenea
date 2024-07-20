using Decenea.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistence.EntityConfigurations.Common;

public class EntityVersionConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> 
    where TEntity : EntityVersion
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);
                
        builder.Property(p => p.Id)
            .HasMaxLength(26);
    }
}