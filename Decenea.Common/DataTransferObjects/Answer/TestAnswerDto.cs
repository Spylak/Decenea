using Decenea.Common.Common;

namespace Decenea.Common.DataTransferObjects.Answer;

public class TestAnswerDto : VersionedDto
{
    public string? Id { get; set; }
    public required string QuestionId { get; set; }
    public string SerializedQuestionContent { get; set; } = string.Empty;
}