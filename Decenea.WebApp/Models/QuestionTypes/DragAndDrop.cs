
using Decenea.Common.Enums;

namespace Decenea.WebApp.Models.QuestionTypes;

public class DragAndDrop : QuestionBase
{
    public DragAndDrop() : base(QuestionType.DragAndDrop)
    {
    }

    private Dictionary<int, string> _choices = new Dictionary<int, string>();
    public List<DropItem> Choices { get; set; } = new List<DropItem>();

    public Dictionary<int, string> DropZones
    {
        get
        {
            return _choices;
        }
        set
        {
            _choices = value;
            _choices.TryAdd(0, "Choices");
        }
    }

    public class DropItem
    {
        public string Name { get; set; }
        public string Selector { get; set; }
    }
}