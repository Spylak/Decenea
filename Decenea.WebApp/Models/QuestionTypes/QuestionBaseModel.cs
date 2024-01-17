
namespace Decenea.WebApp.Models.QuestionTypes;

public class QuestionBaseModel
{
    public string? Id { get; set; } = Guid.NewGuid().ToString();
    public string Question { get; set; } = "";
    public string Title { get; set; } = "";
    public int SecondsToAnswer { get; set; } 
    public int Order { get; set; } 
    public double Weight { get; set; } 
    public string QuestionType { get; set; }
    public List<int>? TestIds { get; set; }
}