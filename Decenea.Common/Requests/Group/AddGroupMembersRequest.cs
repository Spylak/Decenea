using Decenea.Common.DataTransferObjects.Group;

namespace Decenea.Common.Requests.Group;

public class AddGroupMembersRequest
{
    public required string GroupId { get; set; }
    public List<AddGroupMemberDto> GroupMembers { get; set; } = [];
}