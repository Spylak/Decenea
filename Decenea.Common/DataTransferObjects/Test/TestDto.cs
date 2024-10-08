using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Question;

namespace Decenea.Common.DataTransferObjects.Test;

public class TestDto : VersionedDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string UserName { get; set; }
    public string UserId { get; set; }
    public List<QuestionDto> Questions { get; set; } = [];
}