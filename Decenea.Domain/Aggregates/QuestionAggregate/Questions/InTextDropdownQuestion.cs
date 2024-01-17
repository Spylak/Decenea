using Decenea.Domain.Aggregates.QuestionAggregate.Common;

namespace Decenea.Domain.Aggregates.QuestionAggregate.Questions;

public class InTextDropdownQuestion : QuestionBase
{
    public List<SpaceOption> Options { get; set; }
    public class SpaceOption
    {
        public int SpaceNo { get; set; }
        public  List<Choice> Choices { get; set; }
    }
    public class Choice
    {
        public string Text { get; set; }
        public bool Checked { get; set; }
    }
}