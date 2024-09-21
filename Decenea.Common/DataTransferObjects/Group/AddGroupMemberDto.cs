using Decenea.Common.Enums;

namespace Decenea.Common.DataTransferObjects.Group;

public class AddGroupMemberDto
{
    public string? Alias { get; set; }
    public required string GroupUserEmail { get; set; }
    public required GroupRole GroupRole { get; set; }
}