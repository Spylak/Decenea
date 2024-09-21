
using Decenea.Common.Common;

namespace Decenea.Domain.Aggregates.QuestionAggregate.ValueObjects;

public class MultipleChoiceSingle : ValueObject
{
    public List<SubQuestion> SubQuestions { get; set; }

    public class SubQuestion
    {
        public string Text { get; set; }
        public string Picked { get; set; }
        public List<string> Choices { get; set; }
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return SubQuestions;
    }
}