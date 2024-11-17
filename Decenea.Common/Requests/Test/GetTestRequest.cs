namespace Decenea.Common.Requests.Test;

public class GetTestRequest
{
    public required string Id { get; set; }
    public bool IncludeQuestions { get; set; }
    public bool IncludeGroups { get; set; }
    public bool IncludeUsers { get; set; }
}