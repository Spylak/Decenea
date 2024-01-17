using Decenea.Domain.Aggregates.QuestionAggregate.Common;

namespace Decenea.Domain.Aggregates.QuestionAggregate.Questions;

public class MultipleYesOrNoQuestion : QuestionBase
{
    public List<SubQuestion> SubQuestions { get; set; }
    public class SubQuestion
    {
        public string Text { get; set; }
        public bool Checked { get; set; }
    }
}