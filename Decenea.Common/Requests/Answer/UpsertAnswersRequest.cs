using Decenea.Common.DataTransferObjects.Answer;

namespace Decenea.Common.Requests.Answer;

public class UpsertAnswersRequest
{
    public required string TestId { get; set; }
    public required List<TestAnswerDto> Answers { get; set; }
}