using Decenea.Domain.Aggregates.UserAggregate;
using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.GroupAggregate;

public class GroupUser : LinkingTableEntity
{
    public required string UserId { get; set; }
    public required User User { get; set; }
    public required string TestId { get; set; }
    public required Group Group { get; set; }
}