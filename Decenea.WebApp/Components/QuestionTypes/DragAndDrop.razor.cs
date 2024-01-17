using Microsoft.AspNetCore.Components;
using MudBlazor;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class DragAndDrop
{
    [Parameter] public DragAndDropQuestionModel DragAndDropQuestionModel { get; set; } = new DragAndDropQuestionModel();
    [Parameter] public EventCallback<DragAndDropQuestionModel> DragAndDropQuestionModelChanged { get; set; }
    private string DropZoneInput { get; set; } = "";
    private string DropItemInput { get; set; } = "";
    private bool Rerender { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        await ValueChanged();
    }
    
    private void ItemUpdated(MudItemDropInfo<DragAndDropQuestionModel.DropItem> dropItem)
    {
        dropItem.Item.Selector = dropItem.DropzoneIdentifier;
    }
    
    private async Task CreateSample()
    {
        DragAndDropQuestionModel = SampleService.GetDragAndDropQuestionSample();
        await DragAndDropQuestionModelChanged.InvokeAsync(DragAndDropQuestionModel);
        await ValueChanged();
    }
    
    private async Task Reset()
    {
        DragAndDropQuestionModel = new DragAndDropQuestionModel();
        await DragAndDropQuestionModelChanged.InvokeAsync(DragAndDropQuestionModel);
        await ValueChanged();
    }
    
    private async Task AddNewDropZone()
    {
        var identifier = (DragAndDropQuestionModel.DropZones.Max(i => int.Parse(i.Identifier))+1).ToString();
        DragAndDropQuestionModel.DropZones.Add(new DragAndDropQuestionModel.DropZone()
        {
            Identifier = identifier,
            Name = $"DropZone {identifier}"
        });
        await ValueChanged();
    }

    private async Task AddNewField()
    {
        DragAndDropQuestionModel.Choices.Add(new DragAndDropQuestionModel.DropItem()
        {
            Name = $"Item {DateTime.Now.ToString("mm:ss")}",
            Selector = "0"
        });
        await ValueChanged();
    }
    
    private async Task ValueChanged()
    {
        Rerender = false;
        await Task.Delay(50);
        Rerender = true;
    }
    
    private async Task RemoveItem(DragAndDropQuestionModel.DropItem item)
    {
        DragAndDropQuestionModel.Choices.Remove(item);
        await ValueChanged();
    }
    
    private async Task RemoveItem(DragAndDropQuestionModel.DropZone item)
    {
        DragAndDropQuestionModel.DropZones.Remove(item);
        await ValueChanged();
    }
}