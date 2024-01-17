using Decenea.Domain.Aggregates.TestAggregate;
using Decenea.Infrastructure.Persistance.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations;

public class TestConfiguration : AuditableAggregateTypeConfiguration<Test>
{
        public override void Configure(EntityTypeBuilder<Test> builder)
        {
                base.Configure(builder);
                builder.ToTable(name: "Tests");
                
                builder.Property(p => p.Id)
                        .HasMaxLength(26);
        }
}