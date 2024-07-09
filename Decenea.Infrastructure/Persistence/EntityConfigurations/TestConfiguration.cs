using Decenea.Domain.Aggregates.TestAggregate;
using Decenea.Infrastructure.Persistence.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistence.EntityConfigurations;

public class TestConfiguration : AuditableAggregateTypeConfiguration<Test>
{
        public override void Configure(EntityTypeBuilder<Test> builder)
        {
                base.Configure(builder);
                builder.ToTable(name: "Tests");
        }
}