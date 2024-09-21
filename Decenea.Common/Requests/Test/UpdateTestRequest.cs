
namespace Decenea.Common.Requests.Test;

public class UpdateTestRequest
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ContactPhone { get; set; }
    public string ContactEmail { get; set; }
    public string Version { get; set; }
    public List<string>? QuestionIds { get; set; }
}