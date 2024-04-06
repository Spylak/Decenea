using Decenea.WebApp.Helpers;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class DragAndDropQuestion
{
    [Parameter]
    public QuestionBaseModel? DragAndDropQuestionBaseModel { get; set; }
    [Parameter] public EventCallback<QuestionBaseModel> DragAndDropQuestionBaseModelChanged { get; set; }
    private QuestionBaseModel<DragAndDrop>? DragAndDropQuestionModel { get; set; }

    protected override void OnInitialized()
    {
        if (DragAndDropQuestionBaseModel is not null)
        {
            DragAndDropQuestionModel = QuestionBaseModel.ConvertToGeneric<DragAndDrop>(DragAndDropQuestionBaseModel);
        }
        else
        {
            DragAndDropQuestionModel = new QuestionBaseModel<DragAndDrop>(new DragAndDrop());
        }
    }

    private string DropZoneInput { get; set; } = "";
    private string DropItemInput { get; set; } = "";
    private bool Rerender { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        await ValueChanged();
    }
    
    private void ItemUpdated(MudItemDropInfo<DragAndDrop.DropItem> dropItem)
    {
        if (dropItem.Item is null)
            return;
        dropItem.Item.Selector = dropItem.DropzoneIdentifier;
    }
    
    private async Task CreateSample()
    {
        DragAndDropQuestionModel = SampleHelper.GetDragAndDropQuestionSample();
        await DragAndDropQuestionBaseModelChanged.InvokeAsync(QuestionBaseModel.ConvertToNonGeneric(DragAndDropQuestionModel));
        await ValueChanged();
    }
    
    private async Task Reset()
    {
        DragAndDropQuestionModel = new QuestionBaseModel<DragAndDrop>(new DragAndDrop());
        await DragAndDropQuestionBaseModelChanged.InvokeAsync(QuestionBaseModel.ConvertToNonGeneric(DragAndDropQuestionModel));
        await ValueChanged();
    }
    
    private async Task AddNewDropZone()
    {
        if (DragAndDropQuestionModel?.QuestionContent is null)
            return;

        var identifier = string.Empty;
        if (DragAndDropQuestionModel
            .QuestionContent
            .DropZones.Any())
        {
            identifier = (DragAndDropQuestionModel
                    .QuestionContent
                    .DropZones.Max(i => int.Parse(i.Identifier))+1)
                .ToString();
        }
        else
        {
            identifier = "1";
        }
        
        DragAndDropQuestionModel.QuestionContent.DropZones.Add(new DragAndDrop.DropZone()
        {
            Identifier = identifier,
            Name = $"DropZone {identifier}"
        });
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
    
    private async Task RemoveItem(DragAndDrop.DropZone item)
    {
        if (DragAndDropQuestionModel?.QuestionContent is null)
            return;
        
        DragAndDropQuestionModel.QuestionContent.DropZones.Remove(item);
        await ValueChanged();
    }
}