using Decenea.Common.Common;
using Decenea.Domain.Aggregates.QuestionAggregate.Common;

namespace Decenea.Domain.Aggregates.QuestionAggregate.Questions.ValueObjects;

public class MultipleYesOrNo : ValueObject
{
    public List<SubQuestion> SubQuestions { get; set; }
    public class SubQuestion
    {
        public string Text { get; set; }
        public bool Checked { get; set; }
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return SubQuestions;
    }
}