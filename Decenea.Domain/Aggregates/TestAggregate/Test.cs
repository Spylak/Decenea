using Decenea.Domain.Aggregates.QuestionAggregate;
using Decenea.Domain.Aggregates.UserAggregate;
using Decenea.Domain.Common;
using Decenea.Domain.Helpers;

namespace Decenea.Domain.Aggregates.TestAggregate;

public class Test : AuditableAggregateRoot
{
    public required string UserId { get; set; }
    public User? User { get; set; }
    private List<TestQuestion> _testQuestions = new ();
    public IReadOnlyCollection<TestQuestion> TestQuestions => _testQuestions.AsReadOnly();
    
    private List<TestUser> _testUsers = new ();
    public IReadOnlyCollection<TestUser> TestUsers  => _testUsers.AsReadOnly();
    
    private List<TestGroup> _testGroups = new ();
    public IReadOnlyCollection<TestGroup> TestGroups  => _testGroups.AsReadOnly();

    public required string Title { get; set; }
    public required string Description { get; set; }
    public required int MinutesToComplete { get; set; }
    public static Test Create(
        string title, 
        string descripton,
        string userId,
        int minutesToComplete,
        List<Question>? testQuestions = null,
        List<TestUser>? testUsers = null)
    {
        var test = new Test
        {
            Title = title,
            UserId = userId,
            Description = descripton,
            MinutesToComplete = minutesToComplete
        };
        
        if (testQuestions is not null)
        {
            test._testQuestions = testQuestions.Select(i => new TestQuestion()
            {
                Question = i,
                QuestionId = i.Id,
                Test = test,
                TestId = test.Id
            }).ToList();
        }
        
        if (testUsers is not null)
        {
            test._testUsers = testUsers;
        }

        return test;
    }
    
    public static Test Update(Test test, string title, string descripton,
        List<string>? questionIds = null)
    {
        test.Title = title;
        test.Description = descripton;

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

    public static void AddTestGroupToTest(Test test, string groupId)
    {
        var testGroupResult = TestGroup.Create(groupId, test.Id);
        test._testGroups.Add(testGroupResult);
    }
    
    public static void RemoveTestGroupFromTest(Test test, string groupId)
    {
        var index = test._testGroups.FindIndex(i => i.GroupId == groupId);
        if (index != -1)
        {
            test._testGroups.RemoveAt(index);
        }
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