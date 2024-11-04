using Decenea.Domain.Aggregates.QuestionAggregate;
using Decenea.Infrastructure.Persistence.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistence.EntityConfigurations.Question;

public class QuestionAnswerConfiguration : AuditableEntityConfiguration<QuestionAnswer>
{
    public override void Configure(EntityTypeBuilder<QuestionAnswer> builder)
    {
        base.Configure(builder);
        builder.ToTable(name: "QuestionAnswers");
    }
}