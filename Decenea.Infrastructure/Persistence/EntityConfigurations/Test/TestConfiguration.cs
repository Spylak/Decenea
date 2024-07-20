using Decenea.Infrastructure.Persistence.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistence.EntityConfigurations.Test;

public class TestConfiguration : AuditableAggregateConfiguration<Domain.Aggregates.TestAggregate.Test>
{
        public override void Configure(EntityTypeBuilder<Domain.Aggregates.TestAggregate.Test> builder)
        {
                base.Configure(builder);
                builder.ToTable(name: "Tests");

                builder.Property(t => t.Title).HasMaxLength(200).IsRequired();
                builder.Property(t => t.Description).HasMaxLength(500);
                builder.Property(t => t.ContactPhone).HasMaxLength(20);
                builder.Property(t => t.ContactEmail).HasMaxLength(100);
                builder.Property(t => t.UserId).HasMaxLength(50).IsRequired();

                builder.HasMany(t => t.TestQuestions)
                        .WithOne(tq => tq.Test)
                        .HasForeignKey(tq => tq.TestId)
                        .OnDelete(DeleteBehavior.Cascade);
        }
}