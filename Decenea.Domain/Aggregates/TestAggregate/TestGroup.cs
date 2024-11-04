using Decenea.Domain.Aggregates.GroupAggregate;
using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.TestAggregate;

public class TestGroup : LinkingTable
{
    public required string GroupId { get; set; }
    public Group? Group { get; set; }
    public required string TestId { get; set; }
    public Test? Test { get; set; }
    public DateTime EndTime { get; set; } = DateTime.MaxValue;
    public static TestGroup Create(string groupId, string testId)
    {
        var testGroup = new TestGroup()
        {
            GroupId = groupId,
            TestId = testId
        };

        return testGroup;
    } 
}