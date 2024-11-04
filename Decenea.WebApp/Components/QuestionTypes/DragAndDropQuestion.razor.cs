using Decenea.Common.DataTransferObjects.Question.QuestionTypes;
using Decenea.Common.Enums;
using Decenea.WebApp.Helpers;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class DragAndDropQuestion
{
    [Parameter] public GenericQuestionModel? DragAndDropQuestionBaseModel { get; set; }
    [Parameter] public EventCallback<GenericQuestionModel> DragAndDropQuestionBaseModelChanged { get; set; }
    private GenericQuestionModel<DragAndDrop> DragAndDropQuestionModel { get; set; } = InitializeGenericQuestionModel();

    private string DropZoneInput { get; set; } = "";
    private string DropItemInput { get; set; } = "";
    private bool Rerender { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        DragAndDropQuestionModel = GenericQuestionModel.ConvertToGenericModel<DragAndDrop>(DragAndDropQuestionBaseModel ?? InitializeGenericQuestionModel());
        await ValueChanged();
    }

    private void DescriptionChanged(string description)
    {
        DragAndDropQuestionModel.Description = description;
    }

    private async Task ItemUpdated(MudItemDropInfo<DragAndDrop.DropItem> dropItem)
    {
        if (dropItem.Item is null)
            return;
        
        dropItem.Item.Selector = dropItem.DropzoneIdentifier;
        await DragAndDropQuestionBaseModelChanged.InvokeAsync(
            GenericQuestionModel.ConvertToNonGenericModel(DragAndDropQuestionModel));
    }

    private async Task CreateSample()
    {
        DragAndDropQuestionModel = SampleHelper.GetDragAndDropQuestionSample();
        await DragAndDropQuestionBaseModelChanged.InvokeAsync(
            GenericQuestionModel.ConvertToNonGenericModel(DragAndDropQuestionModel));
        await ValueChanged();
    }

    private static GenericQuestionModel<DragAndDrop> InitializeGenericQuestionModel()
    {
        return new GenericQuestionModel<DragAndDrop>(new DragAndDrop())
        {
            QuestionType = QuestionType.DragAndDrop
        };
    }

    private async Task Reset()
    {
        DragAndDropQuestionModel = InitializeGenericQuestionModel();
        await ValueChanged();
    }

    private async Task AddNewDropZone()
    {
        var identifier = DragAndDropQuestionModel
            .QuestionContent
            .DropZones.Keys.Max() + 1;

        DragAndDropQuestionModel.QuestionContent.DropZones[identifier] = $"DropZone {identifier}";
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

    private async Task DragAndDropQuestionModelQuestionContentDropZonesChnaged(int key, string newVal)
    {
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
        DragAndDropQuestionModel.QuestionContent.Choices.Remove(item);
        await ValueChanged();
    }

    private async Task RemoveItem(int key)
    {
        DragAndDropQuestionModel.QuestionContent.DropZones.Remove(key);
        await ValueChanged();
    }
}