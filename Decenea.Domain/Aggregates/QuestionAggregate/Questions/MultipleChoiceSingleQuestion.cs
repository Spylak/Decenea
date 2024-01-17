using Decenea.Domain.Aggregates.QuestionAggregate.Common;

namespace Decenea.Domain.Aggregates.QuestionAggregate.Questions;

public class MultipleChoiceSingleQuestion : QuestionBase
{
    public List<SubQuestion> SubQuestions { get; set; }

    public class SubQuestion
    {
        public string Text { get; set; }
        public string Picked { get; set; }
        public List<string> Choices { get; set; }
    }
}