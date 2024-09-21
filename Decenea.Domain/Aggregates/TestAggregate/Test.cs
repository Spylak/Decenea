using Decenea.Domain.Aggregates.UserAggregate;
using ErrorOr;
using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.TestAggregate;

public class Test : AuditableAggregateRoot
{
    public required string UserId { get; set; }
    public User User { get; set; }
    private List<TestQuestion> _testQuestions = new ();
    public IReadOnlyCollection<TestQuestion> TestQuestions => _testQuestions.AsReadOnly();
    
    private List<TestUser> _testUsers = new ();
    public IReadOnlyCollection<TestUser> TestUsers  => _testUsers.AsReadOnly();

    public string Title { get; set; }
    public string Description { get; set; }
    public string ContactPhone { get; set; }
    public string ContactEmail { get; set; }
    public static Test Create(string title, string descripton,
        string contactEmail,
        string contactPhone,
        string userId,
        List<string> questionIds)
    {
        var test = new Test()
        {
            Title = title,
            UserId = userId,
            Description = descripton,
            ContactEmail = contactEmail,
            ContactPhone = contactPhone
        };
        
        foreach (var questionId in questionIds)
        {
            test.AddQuestion(questionId);
        }

        return test;
    }
    
    public static Test Update(Test test, string title, string descripton,
        string contactEmail,
        string contactPhone,
        List<string>? questionIds = null)
    {
        test.Title = title;
        test.Description = descripton;
        test.ContactEmail = contactEmail;
        test.ContactPhone = contactPhone;

        if (questionIds is not null)
        {
            test._testQuestions.Clear();
        
            foreach (var questionId in questionIds)
            {
                test.AddQuestion(questionId);
            }
        }
        
        return test;
    }

    public void AddQuestion(string questionId)
    {
        _testQuestions.Add(new TestQuestion()
        {
            TestId = Id,
            QuestionId = questionId
        });
    }
    
    public void RemoveQuestion(string questionId)
    {
        _testQuestions = _testQuestions.Where(i => i.QuestionId != questionId).ToList();
    }

    public static void AddTestUserToTest(Test test, string userId)
    {
        var testUserResult = TestUser.Create(userId, test.Id);
        test._testUsers.Add(testUserResult);
    }
    
    public static void RemoveTestUserFromTest(Test test, string userId)
    {
        var index = test._testUsers.FindIndex(i => i.UserId == userId);
        if (index != -1)
        {
            test._testUsers.RemoveAt(index);
        }
    }
}