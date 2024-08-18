using Decenea.Domain.Aggregates.UserAggregate;
using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.GroupAggregate;

public class GroupMember : LinkingTableEntity
{
    public string? Alias { get; set; }
    public required string UserId { get; set; }
    public required User User { get; set; }
    public required string GroupId { get; set; }
    public required Group Group { get; set; }
    public int GroupRoleId { get; set; }
}