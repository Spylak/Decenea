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

                builder.Property(t => t.Title).HasMaxLength(500).IsRequired();
                
                builder.HasOne(tq => tq.User)
                        .WithMany(t => t.Tests)
                        .HasForeignKey(tq => tq.UserId)
                        .OnDelete(DeleteBehavior.SetNull);
                
                builder.HasMany(t => t.TestQuestions)
                        .WithOne(tq => tq.Test)
                        .HasForeignKey(tq => tq.TestId)
                        .OnDelete(DeleteBehavior.Cascade);
        }
}