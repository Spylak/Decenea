using Decenea.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations.Common;

public class BaseEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> 
    where TEntity : Entity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);
                
        builder.Property(p => p.Id)
            .HasMaxLength(26);
    }
}