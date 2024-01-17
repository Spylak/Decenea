using Decenea.Domain.Aggregates.QuestionAggregate.Common;

namespace Decenea.Domain.Aggregates.QuestionAggregate.Questions;

public class FillBlankQuestion : QuestionBase
{
    public List<SpaceOption> Options { get; set; }

    public class SpaceOption
    {
        public int SpaceNo { get; set; }
        public string Text { get; set; }
    }
}