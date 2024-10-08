using ErrorOr;
using Decenea.Domain.Aggregates.UserAggregate;
using Decenea.Domain.Common;
using Decenea.Domain.Helpers;

namespace Decenea.Domain.Aggregates.TestAggregate;

public class TestUser : LinkingTable
{
    public required string UserId { get; set; }
    public User? User { get; set; }
    public required string TestId { get; set; }
    public Test? Test { get; set; }

    public static TestUser Create(string userId, string testId)
    {
        var testUser = new TestUser()
        {
            UserId = userId,
            TestId = testId,
            Version = RandomStringGenerator.RandomString(8)
        };

        return testUser;
    } 
}