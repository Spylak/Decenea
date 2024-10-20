using Decenea.Common.Enums;
using Decenea.Domain.Aggregates.TestAggregate;
using Decenea.Domain.Aggregates.UserAggregate;
using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.QuestionAggregate;

public class Question : AuditableAggregateRoot
{
    public required string UserId { get; set; }
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

    public static Question Create(string description,
        bool isAnswer,
        string title,
        int? secondsToAnswer,
        double? weight,
        int? order,
        string userId,
        QuestionType questionType,
        string serializedQuestionContent,
        string? testId = null
        )
    {
        var question = new Question
        {
            Description = description,
            Title = title,
            UserId = userId,
            QuestionType = questionType,
            SerializedQuestionContent = serializedQuestionContent,
            Weight = weight,
            IsAnswer = isAnswer,
            SecondsToAnswer = secondsToAnswer,
            Order = order
        };
        
        if (testId != null)
        {
            question._testQuestions.Add(new TestQuestion()
            {
                TestId = testId,
                QuestionId = question.Id
            });
        }
        return question;
    }
}