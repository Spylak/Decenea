using Decenea.Domain.Aggregates.QuestionAggregate.Common;

namespace Decenea.Domain.Aggregates.QuestionAggregate.Questions;

public class OrderingQuestion : QuestionBase
{
    public List<Choice> Choices { get; set; }

    public class Choice
    {
        public int Order { get; set; }
        public bool Active { get; set; }
        public string Text { get; set; }
    }
}