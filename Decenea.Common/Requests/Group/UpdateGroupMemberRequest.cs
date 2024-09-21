using Decenea.Common.DataTransferObjects.Group;

namespace Decenea.Common.Requests.Group;

public class UpdateGroupMemberRequest
{
    public required string GroupId { get; set; }
    public UpdateGroupMemberDto? UpdateGroupMemberDto { get; set; }
}