using Decenea.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistence.EntityConfigurations.Common;

public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> 
    where TEntity : Entity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);
                
        builder.Property(p => p.Id)
            .HasMaxLength(26);
        
        builder.Property(p => p.Version)
            .HasMaxLength(8)
            .IsConcurrencyToken();
    }
}