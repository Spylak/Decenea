namespace Decenea.Common.Requests.Group;

public class RemoveGroupMemberRequest
{
    public required string GroupUserId { get; set; }
    public required string GroupId { get; set; }
    public required string UserEmail { get; set; }
}