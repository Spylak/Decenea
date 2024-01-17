using Decenea.Domain.Aggregates.QuestionAggregate.Common;

namespace Decenea.Domain.Aggregates.QuestionAggregate.Questions;

public class DropdownQuestion : QuestionBase
{
    public List<SubQuestion> SubQuestions { get; set; }
    public class SubQuestion
    {
        public string Text { get; set; }
        public List<Choice> Choices { get; set; }
    }
    public class Choice
    {
        public string Text { get; set; }
        public bool Checked { get; set; }
    }
}