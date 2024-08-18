using Decenea.Common.Common;
using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.TestAggregate;

public class Test : AuditableAggregateRoot
{
    private List<TestQuestion> _testQuestions = new ();
    public IReadOnlyCollection<TestQuestion> TestQuestions => _testQuestions.AsReadOnly();
    
    private List<TestUser> _testUsers = new ();
    public IReadOnlyCollection<TestUser> TestUsers  => _testUsers.AsReadOnly();

    public string Title { get; set; }
    public string Description { get; set; }
    public string ContactPhone { get; set; }
    public string ContactEmail { get; set; }
    public static Result<Test, Exception> Create(string title, string descripton,
        string contactEmail,
        string contactPhone)
    {
        var test = new Test()
        {
            Title = title,
            Description = descripton,
            ContactEmail = contactEmail,
            ContactPhone = contactPhone
        };
        
        return Result<Test, Exception>.Anticipated(test);
    }
    
    public static Result<Test, Exception> Update(Test test, string title, string descripton,
        string contactEmail,
        string contactPhone)
    {
        test.Title = title;
        test.Description = descripton;
        test.ContactEmail = contactEmail;
        test.ContactPhone = contactPhone;
        
        return Result<Test, Exception>.Anticipated(test);
    }

    public static void AddTestUserToTest(Test test, string userId)
    {
        var testUserResult = TestUser.Create(userId, test.Id);
        if (testUserResult.SuccessValue is not null)
        {
            test._testUsers.Add(testUserResult.SuccessValue);
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