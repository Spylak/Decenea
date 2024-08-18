using Decenea.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistence.EntityConfigurations.Common;

public class VersionedEntityConfiguration<TEntity> : BaseEntityConfiguration<TEntity> 
    where TEntity : VersionedEntity
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);
        
        builder.Property(p => p.Version)
            .IsRequired()
            .HasMaxLength(8)
            .IsConcurrencyToken();
    }
}