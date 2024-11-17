using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Test;

namespace Decenea.Common.DataTransferObjects.Group;

public class GroupDto : VersionedDto
{
    public string Id { get; set; }
    public required string Name { get; set; }
    public List<GroupMemberDto> GroupMembers { get; set; } = [];
    public List<TestDto> TestDtos { get; set; } = [];
}