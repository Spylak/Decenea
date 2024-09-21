namespace Decenea.Common.DataTransferObjects.Group;

public class GroupDto
{
    public string Id { get; set; }
    public required string Name { get; set; }
    public string Version { get; set; }
    public List<GroupMemberDto> GroupMembers { get; set; } = [];
}