using Decenea.Domain.Aggregates.TestAggregate;
using Decenea.Infrastructure.Persistence.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistence.EntityConfigurations.Test;

public class TestQuestionConfiguration : LinkingTableConfiguration<TestQuestion>
{
    public override void Configure(EntityTypeBuilder<TestQuestion> builder)
    {
        base.Configure(builder);
        builder.ToTable(name: "TestQuestions");

        builder.HasKey(tq => new { tq.TestId, tq.QuestionId });

        builder.Property(tq => tq.TestId).IsRequired();
        builder.Property(tq => tq.QuestionId).IsRequired();

        builder.HasOne(tq => tq.Test)
            .WithMany(t => t.TestQuestions)
            .HasForeignKey(tq => tq.TestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(tq => tq.Question)
            .WithMany(q => q.TestQuestions)
            .HasForeignKey(tq => tq.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}