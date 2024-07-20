
using Decenea.Common.Enums;
using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.TestAggregate.Questions;

public class Question : AuditableEntity
{
    private List<TestQuestion> _testQuestions = new ();
    public IReadOnlyCollection<TestQuestion> TestQuestions => _testQuestions.AsReadOnly();
    public string Desription { get; set; }
    public string Title { get; set; }
    public int SecondsToAnswer { get; set; } 
    public int Order { get; set; } 
    public double Weight { get; set; }
    public QuestionType QuestionType { get; set; }
    public string SerializedQuestionContent { get; set; } = string.Empty;
}