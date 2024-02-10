using Decenea.WebApp.Helpers;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class DragAndDropQuestion
{
    [Parameter]
    public QuestionBaseModel<DragAndDrop> DragAndDropQuestionModel { get; set; } = new QuestionBaseModel<DragAndDrop>(new DragAndDrop());
    [Parameter] public EventCallback<QuestionBaseModel<DragAndDrop>> DragAndDropQuestionModelChanged { get; set; }
    private string DropZoneInput { get; set; } = "";
    private string DropItemInput { get; set; } = "";
    private bool Rerender { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        await ValueChanged();
    }
    
    private void ItemUpdated(MudItemDropInfo<DragAndDrop.DropItem> dropItem)
    {
        dropItem.Item.Selector = dropItem.DropzoneIdentifier;
    }
    
    private async Task CreateSample()
    {
        DragAndDropQuestionModel = SampleHelper.GetDragAndDropQuestionSample();
        await DragAndDropQuestionModelChanged.InvokeAsync(DragAndDropQuestionModel);
        await ValueChanged();
    }
    
    private async Task Reset()
    {
        DragAndDropQuestionModel = new QuestionBaseModel<DragAndDrop>(new DragAndDrop());
        await DragAndDropQuestionModelChanged.InvokeAsync(DragAndDropQuestionModel);
        await ValueChanged();
    }
    
    private async Task AddNewDropZone()
    {
        var identifier = (DragAndDropQuestionModel.QuestionContent.DropZones.Max(i => int.Parse(i.Identifier))+1).ToString();
        DragAndDropQuestionModel.QuestionContent.DropZones.Add(new DragAndDrop.DropZone()
        {
            Identifier = identifier,
            Name = $"DropZone {identifier}"
        });
        await ValueChanged();
    }

    private async Task AddNewField()
    {
        DragAndDropQuestionModel.QuestionContent.Choices.Add(new DragAndDrop.DropItem()
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
    
    private async Task RemoveItem(DragAndDrop.DropItem item)
    {
        DragAndDropQuestionModel.QuestionContent.Choices.Remove(item);
        await ValueChanged();
    }
    
    private async Task RemoveItem(DragAndDrop.DropZone item)
    {
        DragAndDropQuestionModel.QuestionContent.DropZones.Remove(item);
        await ValueChanged();
    }
}