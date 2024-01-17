using Decenea.Domain.Aggregates.QuestionAggregate.Common;

namespace Decenea.Domain.Aggregates.QuestionAggregate.Questions;

public class OrderingDnDQuestion : QuestionBase
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
}