using Decenea.Common.Enums;

namespace Decenea.Common.DataTransferObjects.Group;

public class UpdateGroupMemberDto
{
    public string? Alias { get; set; }
    public required string Version { get; set; }
    public required string GroupUserEmail { get; set; }
    public required GroupRole GroupRole { get; set; }
}