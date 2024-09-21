using Decenea.Common.Common;

namespace Decenea.Domain.Aggregates.QuestionAggregate.ValueObjects;

public class InTextDropdown : ValueObject
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

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Options;
    }
}