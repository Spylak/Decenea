using Decenea.Common.DataTransferObjects.Answer;

namespace Decenea.Common.DataTransferObjects.Test;

public class ActiveTestDto : BaseTestDto
{
    public List<TestAnswerDto> TestAnswers { get; set; } = [];
}