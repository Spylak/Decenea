using Decenea.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistence.EntityConfigurations.Common;

public class LinkingTableConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> 
    where TEntity : LinkingTableEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
    }
}