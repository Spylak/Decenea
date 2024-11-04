using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Question;

namespace Decenea.Common.DataTransferObjects.Test;

public class BaseTestDto : VersionedDto
{
    public string Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required int MinutesToComplete { get; set; }
    public DateTime? StartingTime { get; set; }
    public string UserName { get; set; }
    public string UserId { get; set; }
    public List<QuestionDto> Questions { get; set; } = [];
}