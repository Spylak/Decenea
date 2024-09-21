using Decenea.Common.DataTransferObjects.Question;

namespace Decenea.Common.Requests.Test;

public record CreateTestRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ContactPhone { get; set; }
    public string ContactEmail { get; set; }
    public List<QuestionDto> Questions { get; set; }
}