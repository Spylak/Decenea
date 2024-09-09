namespace Decenea.Common.Requests.Group;

public class RemoveGroupMemberRequest
{
    public required string GroupUserEmail { get; set; }
    public required string GroupId { get; set; }
}