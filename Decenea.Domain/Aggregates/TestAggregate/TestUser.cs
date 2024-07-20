using Decenea.Domain.Aggregates.UserAggregate;
using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.TestAggregate;

public class TestUser : LinkingTableEntity
{
    public required string UserId { get; set; }
    public required User User { get; set; }
    public required string TestId { get; set; }
    public required Test Test { get; set; }
}