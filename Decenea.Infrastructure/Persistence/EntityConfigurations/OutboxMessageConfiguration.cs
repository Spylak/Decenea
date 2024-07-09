using Decenea.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistence.EntityConfigurations;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage> 
{
    public virtual void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.HasKey(e => e.Id);
                
        builder.Property(p => p.Id)
            .HasMaxLength(26);
        
        builder.Property(p => p.Version)
            .HasMaxLength(8)
            .IsRowVersion();
    }
}