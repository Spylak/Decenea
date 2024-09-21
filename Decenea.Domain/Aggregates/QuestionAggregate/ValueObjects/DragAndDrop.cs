using Decenea.Common.Common;

namespace Decenea.Domain.Aggregates.QuestionAggregate.ValueObjects;

public class DragAndDrop : ValueObject
{
    public List<DropItem> Choices { get; set; } 
    public List<DropZone> DropZones { get; set; }
    public class DropZone
    {
        public string Name { get; set; }
        public string Identifier { get; set; }
    }
    public class DropItem
    {
        public string Name { get; set; }
        public string Selector { get; set; }
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Choices;
        yield return DropZones;
    }
}