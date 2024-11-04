

using Decenea.Common.Enums;

namespace Decenea.Common.DataTransferObjects.Question.QuestionTypes;

public class OrderingDragAndDrop : QuestionBase
{
    public OrderingDragAndDrop() : base(QuestionType.OrderingDragAndDrop)
    {
        
    }
    public List<DropItem> Choices { get; set; } = new List<DropItem>();
    public DropZone AnswerZone { get; set; } = new DropZone()
    {
        Name = "Answers",
        Identifier = "1"
    };
    public DropZone OptionZone { get; set; } = new DropZone()
    {
        Name = "Options",
        Identifier = "0"
    };
    public class DropZone
    {
        public string Name { get; set; }
        public string Identifier { get; set; }
    }
    public class DropItem
    {
        public DropItem()
        {
        }
        public DropItem(int showOrder)
        {
            Order = showOrder;
        }
        public string Name { get; set; }
        public string Selector { get; set; }
        public int Order { get; set; }
    }
}