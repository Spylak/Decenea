using Decenea.Common.Enums;
using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.GroupAggregate;

public class GroupMember : LinkingTable
{
    public string? Alias { get; set; }
    public required string GroupUserEmail { get; set; }
    public required string GroupId { get; set; }
    public Group Group { get; set; }
    public required GroupRole GroupRole { get; set; }
}