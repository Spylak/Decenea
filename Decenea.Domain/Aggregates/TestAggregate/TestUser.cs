using Decenea.Common.Common;
using Decenea.Domain.Aggregates.UserAggregate;
using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.TestAggregate;

public class TestUser : LinkingTableEntity
{
    public required string UserId { get; set; }
    public User? User { get; set; }
    public required string TestId { get; set; }
    public Test? Test { get; set; }

    public static Result<TestUser, Exception> Create(string userId, string testId)
    {
        var testUser = new TestUser()
        {
            UserId = userId,
            TestId = testId
        };
        
        return Result<TestUser, Exception>.Anticipated(testUser);
    } 
}