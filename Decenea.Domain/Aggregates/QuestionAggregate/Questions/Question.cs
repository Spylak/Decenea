
using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.QuestionAggregate.Common;

public class Question<T> : Entity where T : class 
{
    private readonly List<string> _testIds = new ();
    public string Desription { get; set; }
    public string Title { get; set; }
    public int SecondsToAnswer { get; set; } 
    public int Order { get; set; } 
    public double Weight { get; set; }
    public string QuestionType { get; set; }
    public T QuestionContent { get; set; }
}