using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Models;

public class TestModel
{
    public string Id { get; set; } = Ulid.NewUlid().ToString();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public int MinutesToComplete { get; set; }
    public DateTime? StartingTime { get; set; }
    public DateTime? FinishTime => StartingTime?.AddMinutes(MinutesToComplete);
    public bool IsActive { get; set; }
    public bool IsDraft { get; set; }
    public List<GenericQuestionModel> GenericQuestionModels { get; set; } = new List<GenericQuestionModel>();
}