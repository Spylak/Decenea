using Decenea.Domain.Aggregates.TestAggregate.Questions;
using Decenea.Infrastructure.Persistence.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistence.EntityConfigurations.Test;

public class QuestionConfiguration : AuditableEntityConfiguration<Question>
{
        public override void Configure(EntityTypeBuilder<Question> builder)
        {
                base.Configure(builder);
                builder.ToTable(name: "Questions");
                
                builder.Property(q => q.Desription).HasColumnType("nvarchar(max)").IsRequired();
                builder.Property(q => q.Title).HasMaxLength(200).IsRequired();
                builder.Property(q => q.QuestionType).IsRequired();
                builder.Property(q => q.SerializedQuestionContent).HasColumnType("nvarchar(max)");

                builder.HasMany(q => q.TestQuestions)
                        .WithOne(tq => tq.Question)
                        .HasForeignKey(tq => tq.QuestionId)
                        .OnDelete(DeleteBehavior.Cascade);
        }
}