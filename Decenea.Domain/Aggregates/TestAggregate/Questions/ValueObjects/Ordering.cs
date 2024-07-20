
using Decenea.Common.Common;

namespace Decenea.Domain.Aggregates.TestAggregate.Questions.ValueObjects;

public class Ordering : ValueObject
{
    public List<Choice> Choices { get; set; }

    public class Choice
    {
        public int Order { get; set; }
        public bool Active { get; set; }
        public string Text { get; set; }
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Choices;
    }
}