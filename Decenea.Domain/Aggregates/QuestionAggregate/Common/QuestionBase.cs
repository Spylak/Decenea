
namespace Decenea.Domain.Aggregates.QuestionAggregate.Common;

public class QuestionBase
{
    public string Id { get; set; }
    public string Question { get; set; }
    public string Title { get; set; }
    public int SecondsToAnswer { get; set; } 
    public int Order { get; set; } 
    public double Weight { get; set; }
    public string QuestionType { get; set; }
    public List<int>? TestIds { get; set; }
}