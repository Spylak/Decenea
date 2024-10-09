
using Decenea.Common.DataTransferObjects.Question;

namespace Decenea.Common.Requests.Test;

public class UpdateTestRequest
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Version { get; set; }
    public required int MinutesToComplete { get; set; }
    public List<QuestionDto>? Questions { get; set; }
}