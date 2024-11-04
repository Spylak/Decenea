using Decenea.Domain.Aggregates.QuestionAggregate;
using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.TestAggregate;

public class TestAnswer : AuditableEntity
{
    public required string TestUserId { get; set; }
    public TestUser? TestUser { get; set; }
    public required string QuestionId { get; set; }
    public string SerializedQuestionContent { get; set; } = string.Empty;
}