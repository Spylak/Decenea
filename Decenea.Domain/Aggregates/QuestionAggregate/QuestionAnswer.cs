using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.QuestionAggregate;

public class QuestionAnswer : AuditableEntity
{
    
    public required string QuestionId { get; set; }
    public Question? Question { get; set; }
    public required string SerializedAnsweredContent { get; set; }
}