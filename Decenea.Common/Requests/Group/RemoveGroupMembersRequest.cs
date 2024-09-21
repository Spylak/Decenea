namespace Decenea.Common.Requests.Group;

public class RemoveGroupMembersRequest
{
    public List<string> GroupUserEmails { get; set; }
    public required string GroupId { get; set; }
}