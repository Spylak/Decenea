using Decenea.Common.Enums;
using Decenea.WebApp.Helpers;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class DragAndDropQuestion
{
    [Parameter]
    public GenericQuestionModel? DragAndDropQuestionBaseModel { get; set; }
    [Parameter] public EventCallback<GenericQuestionModel> DragAndDropQuestionBaseModelChanged { get; set; }
    private GenericQuestionModel<DragAndDrop>? DragAndDropQuestionModel { get; set; }

    private string DropZoneInput { get; set; } = "";
    private string DropItemInput { get; set; } = "";
    private bool Rerender { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (DragAndDropQuestionBaseModel is not null)
        {
            DragAndDropQuestionModel = GenericQuestionModel.ConvertToGenericModel<DragAndDrop>(DragAndDropQuestionBaseModel);
        }
        await ValueChanged();
    }
    
    private async Task ItemUpdated(MudItemDropInfo<DragAndDrop.DropItem> dropItem)
    {
        if (dropItem.Item is null)
            return;
        dropItem.Item.Selector = dropItem.DropzoneIdentifier;
        await DragAndDropQuestionBaseModelChanged.InvokeAsync(GenericQuestionModel.ConvertToNonGenericModel(DragAndDropQuestionModel));
    }
    
    private async Task CreateSample()
    {
        DragAndDropQuestionModel = SampleHelper.GetDragAndDropQuestionSample();
        await DragAndDropQuestionBaseModelChanged.InvokeAsync(GenericQuestionModel.ConvertToNonGenericModel(DragAndDropQuestionModel));
        await ValueChanged();
    }
    
    private async Task Reset()
    {
        DragAndDropQuestionModel = new GenericQuestionModel<DragAndDrop>(new DragAndDrop())
        {
            QuestionType = QuestionType.DragAndDrop,
            Id = DragAndDropQuestionBaseModel?.Id ?? Ulid.NewUlid().ToString()
        };
        await ValueChanged();
    }
    
    private async Task AddNewDropZone()
    {
        if (DragAndDropQuestionModel?.QuestionContent is null)
            return;

        var identifier = DragAndDropQuestionModel
            .QuestionContent
            .DropZones.Keys.Max() + 1;

        DragAndDropQuestionModel.QuestionContent.DropZones[identifier] = $"DropZone {identifier}";
        await ValueChanged();
    }

    private async Task AddNewField()
    {
        if (DragAndDropQuestionModel?.QuestionContent is null)
            return;
        
        DragAndDropQuestionModel.QuestionContent.Choices.Add(new DragAndDrop.DropItem()
        {
            Name = $"Item {DateTime.Now.ToString("mm:ss")}",
            Selector = "0"
        });
        await ValueChanged();
    }

    private async Task DragAndDropQuestionModelQuestionContentDropZonesChnaged(int key, string newVal)
    {
        if (DragAndDropQuestionModel?.QuestionContent is null)
            return;
        
        DragAndDropQuestionModel.QuestionContent.DropZones[key] = newVal; 
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
        if (DragAndDropQuestionModel?.QuestionContent is null)
            return;
        
        DragAndDropQuestionModel.QuestionContent.Choices.Remove(item);
        await ValueChanged();
    }
    
    private async Task RemoveItem(int key)
    {
        if (DragAndDropQuestionModel?.QuestionContent is null)
            return;

        DragAndDropQuestionModel.QuestionContent.DropZones.Remove(key);
        await ValueChanged();
    }
}