using Decenea.Domain.Aggregates.TestAggregate.Questions;
using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.TestAggregate;

public class TestQuestion : Auditable
{
    public required string QuestionId { get; set; }
    public required Question Question { get; set; }
    public required string TestId { get; set; }
    public required Test Test { get; set; }
}