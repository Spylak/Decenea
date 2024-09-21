using Decenea.Common.Enums;
using Decenea.Domain.Aggregates.TestAggregate;
using Decenea.Domain.Aggregates.UserAggregate;
using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.QuestionAggregate;

public class Question : AuditableAggregateRoot
{
    public string? UserId { get; set; }
    public User? User { get; set; }
    private List<TestQuestion> _testQuestions = new ();
    public IReadOnlyCollection<TestQuestion> TestQuestions => _testQuestions.AsReadOnly();
    public required string Description { get; set; }
    public required string Title { get; set; }
    public int? SecondsToAnswer { get; set; } 
    public int? Order { get; set; } 
    public double? Weight { get; set; }
    public bool IsAnswer { get; set; }
    public required QuestionType QuestionType { get; set; }
    public required string SerializedQuestionContent { get; set; }
}