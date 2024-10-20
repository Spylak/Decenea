using Decenea.Common.DataTransferObjects.Question;

namespace Decenea.Common.Requests.Question;

public class CreateQuestionsRequest
{
    public required List<QuestionDto> Questions { get; set; }
    public string? TestId { get; set; }
}