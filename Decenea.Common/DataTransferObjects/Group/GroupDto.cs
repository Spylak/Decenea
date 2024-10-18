using Decenea.Common.Common;

namespace Decenea.Common.DataTransferObjects.Group;

public class GroupDto : VersionedDto
{
    public string Id { get; set; }
    public required string Name { get; set; }
    public List<GroupMemberDto> GroupMembers { get; set; } = [];
}