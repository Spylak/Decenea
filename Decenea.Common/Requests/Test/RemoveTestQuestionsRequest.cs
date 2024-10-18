namespace Decenea.Common.Requests.Test;

public class RemoveTestQuestionsRequest
{
    public required string TestId { get; set; }
    public List<string> QuestionIds { get; set; } = [];
}