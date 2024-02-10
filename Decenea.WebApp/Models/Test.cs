using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Models;

public class Test
{
    public string? Id { get; set; } = Guid.NewGuid().ToString();
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int MinutesToComplete { get; set; }
    public DateTime StartingTime { get; set; }
    public DateTime FinishTime => StartingTime.AddMinutes(MinutesToComplete);
    public bool IsActive { get; set; }
    public bool IsDraft { get; set; }
    public List<QuestionBaseModel> QuestionBaseModels { get; set; } = new List<QuestionBaseModel>();
}