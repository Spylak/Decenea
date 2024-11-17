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
        List<Question>? questions = null)
    {
        test.Title = title;
        test.Description = descripton;

        if (questions != null)
        {
            test.SyncQuestions(questions);
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
    
    public void SyncQuestions(List<Question> newQuestions)
    {
        var newQuestionIds = newQuestions
            .Select(q => q.Id)
            .ToHashSet();
        
        var currentQuestionIds = _testQuestions
            .Select(tq => tq.QuestionId)
            .ToHashSet();

        var questionsToRemove = _testQuestions
            .Where(tq => !newQuestionIds.Contains(tq.QuestionId))
            .ToList();
        
        var questionsToUpdate = _testQuestions
            .Where(tq => newQuestionIds.Contains(tq.QuestionId))
            .ToList();
        
        foreach (var questionToRemove in questionsToRemove)
        {
            _testQuestions.Remove(questionToRemove);
        }

        foreach (var questionToUpdate in questionsToUpdate)
        {
            var question = newQuestions
                .First(q => q.Id == questionToUpdate.QuestionId);
            questionToUpdate.Question?.Update(question.Description,
                question.Title, 
                question.Answer?.SerializedAnsweredContent);
        }

        var questionsToAdd = newQuestions
            .Where(q => !currentQuestionIds.Contains(q.Id))
            .Select(q => new TestQuestion
            {
                TestId = Id,
                QuestionId = q.Id,
                Question = q,
                Test = this
            })
            .ToList();
        
        _testQuestions.AddRange(questionsToAdd);
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