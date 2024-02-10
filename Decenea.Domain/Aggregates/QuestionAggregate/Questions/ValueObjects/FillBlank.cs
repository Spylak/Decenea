using Decenea.Common.Common;

namespace Decenea.Domain.Aggregates.QuestionAggregate.Questions.ValueObjects;

public class FillBlank : ValueObject
{
    public List<SpaceOption> Options { get; set; }

    public class SpaceOption
    {
        public int SpaceNo { get; set; }
        public string Text { get; set; }
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Options;
    }
}