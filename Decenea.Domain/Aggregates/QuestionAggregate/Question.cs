using Decenea.Common.Enums;
using Decenea.Domain.Aggregates.TestAggregate;
using Decenea.Domain.Aggregates.UserAggregate;
using Decenea.Domain.Common;
using Decenea.Domain.Helpers;

namespace Decenea.Domain.Aggregates.QuestionAggregate;

public class Question : AuditableAggregateRoot
{
    public required string UserId { get; set; }
    public QuestionAnswer? Answer { get; set; }
    private List<TestQuestion> _testQuestions = new ();
    public IReadOnlyCollection<TestQuestion> TestQuestions => _testQuestions.AsReadOnly();
    public required string Description { get; set; }
    public required string Title { get; set; }
    public required QuestionType QuestionType { get; set; }
    public required string SerializedUnAnsweredContent { get; set; }

    public static Question Create(string description,
        string title,
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
            SerializedUnAnsweredContent = QuestionHelper.SetUnAnsweredContent(questionType, serializedQuestionContent),
        };
        question.Answer = new QuestionAnswer()
        {
            QuestionId = question.Id,
            Question = question,
            SerializedAnsweredContent = serializedQuestionContent
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
    
    public Question Update(
        string description,
        string title,
        string serializedAnsweredContent)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty", nameof(description));
        
        if (string.IsNullOrWhiteSpace(serializedAnsweredContent))
            throw new ArgumentException("Question content cannot be empty", nameof(serializedAnsweredContent));

        Description = description;
        Title = title;
        if (Answer?.SerializedAnsweredContent != serializedAnsweredContent)
        {
            SerializedUnAnsweredContent = QuestionHelper.SetUnAnsweredContent(QuestionType, serializedAnsweredContent);
            if (Answer != null)
            {
                Answer.SerializedAnsweredContent = serializedAnsweredContent;
            }
        }
        return this;
    }
}