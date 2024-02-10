

using Decenea.Common.Constants;

namespace Decenea.WebApp.Models.QuestionTypes;

public class DragAndDrop
{
    public DragAndDrop(List<DropZone>? dropzones = null)
    {
        DropZones = new List<DropZone>()
        {
            new DropZone(){Identifier = "0",Name = "Choices"}
        };
        foreach (var dropzone in dropzones ??= new List<DropZone>())
        {
            dropzone.Identifier = (dropzones.IndexOf(dropzone)+1).ToString();
        }
        DropZones.AddRange(dropzones);
    }
    
    public List<DropItem> Choices { get; set; } = new List<DropItem>();
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