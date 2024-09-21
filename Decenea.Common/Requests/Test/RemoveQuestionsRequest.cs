using Decenea.Common.DataTransferObjects.Question;

namespace Decenea.Common.Requests.Test;

public class RemoveQuestionsRequest
{
    public required string TestId { get; set; }
    public List<string> QuestionIds { get; set; } = [];
}