using Decenea.Infrastructure.Persistence.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistence.EntityConfigurations.Test;

public class TestAnswerConfiguration : AuditableEntityConfiguration<Domain.Aggregates.TestAggregate.TestAnswer>
{
        public override void Configure(EntityTypeBuilder<Domain.Aggregates.TestAggregate.TestAnswer> builder)
        {
                base.Configure(builder);
                builder.ToTable(name: "TestAnswers");

                builder.Property(q => q.SerializedQuestionContent);
        }
}