using Decenea.Domain.Aggregates.QuestionAggregate.Common;

namespace Decenea.Domain.Aggregates.QuestionAggregate.Questions;

public class DragAndDropQuestion : QuestionBase
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
}