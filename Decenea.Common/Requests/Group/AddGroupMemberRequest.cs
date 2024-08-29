using Decenea.Common.Enums;

namespace Decenea.Common.Requests.Group;

public class AddGroupMemberRequest
{
    public required string GroupId { get; set; }
    public required string GroupUserEmail { get; set; }
    public required GroupRole GroupRole { get; set; }
}