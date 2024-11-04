using Decenea.Domain.Aggregates.UserAggregate;
using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.TestAggregate;

public class TestUser : AuditableEntity
{
    public required string UserId { get; set; }
    public User? User { get; set; }
    public required string TestId { get; set; }
    public Test? Test { get; set; }
    public List<TestAnswer> TestAnswers { get; set; } = new ();
    
    public DateTime EndTime { get; set; } = DateTime.MaxValue;
    public static TestUser Create(string userId, string testId)
    {
        var testUser = new TestUser()
        {
            UserId = userId,
            TestId = testId
        };

        return testUser;
    } 
}