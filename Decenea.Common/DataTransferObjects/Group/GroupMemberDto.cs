using Decenea.Common.Enums;

namespace Decenea.Common.DataTransferObjects.Group;

public class GroupMemberDto
{
    public string? Alias { get; set; }
    public required string GroupUserEmail { get; set; }
    public required string GroupId { get; set; }
    public required GroupRole GroupRole { get; set; }
    public string? Version { get; set; }
}