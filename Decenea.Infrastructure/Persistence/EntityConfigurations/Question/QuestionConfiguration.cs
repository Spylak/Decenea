using Decenea.Domain.Aggregates.QuestionAggregate;
using Decenea.Infrastructure.Persistence.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistence.EntityConfigurations.Question;

public class QuestionConfiguration : AuditableAggregateConfiguration<Domain.Aggregates.QuestionAggregate.Question>
{
        public override void Configure(EntityTypeBuilder<Domain.Aggregates.QuestionAggregate.Question> builder)
        {
                base.Configure(builder);
                builder.ToTable(name: "Questions");
                
                builder.Property(q => q.Description).IsRequired();
                builder.Property(q => q.Title).HasMaxLength(200).IsRequired();
                builder.Property(q => q.QuestionType).IsRequired();
                builder.Property(q => q.SerializedUnAnsweredContent);

                builder.HasMany(q => q.TestQuestions)
                        .WithOne(tq => tq.Question)
                        .HasForeignKey(tq => tq.QuestionId)
                        .OnDelete(DeleteBehavior.Cascade);
                
                builder.HasOne(tq => tq.Answer)
                        .WithOne(t => t.Question)
                        .HasForeignKey<QuestionAnswer>(t => t.QuestionId);
        }
}