namespace Decenea.Common.Requests.Group;

public class SyncTestsRequest
{
    public required string GroupId { get; set; }
    public required List<string> TestIds { get; set; }
}