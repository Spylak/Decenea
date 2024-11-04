using Decenea.Domain.Aggregates.UserAggregate;
using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.TestAggregate;

public class TestUser : AuditableEntity
{
    public required string UserId { get; set; }
    public User? User { get; set; }
    public required string TestId { get; set; }
    public Test? Test { get; set; }
    private List<TestAnswer> _testAnswers = new ();
    public IReadOnlyCollection<TestAnswer> TestAnswers => _testAnswers.AsReadOnly();
    
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