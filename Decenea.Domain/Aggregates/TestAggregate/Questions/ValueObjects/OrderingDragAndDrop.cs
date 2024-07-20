using Decenea.Common.Common;

namespace Decenea.Domain.Aggregates.TestAggregate.Questions.ValueObjects;

public class OrderingDnD : ValueObject
{
    public List<DropItem> Choices { get; set; } 
    public DropZone AnswerZone { get; set; } 
    public DropZone OptionZone { get; set; } 
    public class DropZone
    {
        public string Name { get; set; }
        public string Identifier { get; set; }
    }
    public class DropItem
    {
        public string Name { get; set; }
        public string Selector { get; set; }
        public int Order { get; set; }
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Choices;
        yield return AnswerZone;
        yield return OptionZone;
    }
}