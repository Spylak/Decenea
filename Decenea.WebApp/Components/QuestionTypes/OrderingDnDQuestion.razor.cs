using Decenea.Common.DataTransferObjects.Question.QuestionTypes;
using Decenea.Common.Enums;
using Decenea.WebApp.Helpers;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Utilities;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class OrderingDnDQuestion
{
    [Parameter] public GenericQuestionModel? OrderingDnDQuestionBaseModel { get; set; }
    [Parameter] public EventCallback<GenericQuestionModel> OrderingDnDQuestionBaseModelChanged { get; set; }

    private GenericQuestionModel<OrderingDragAndDrop> OrderingDnDQuestionModel { get; set; } = InitializeGenericQuestionModel();

    private bool Rerender { get; set; } = false;

    protected override async Task OnParametersSetAsync()
    {
        OrderingDnDQuestionModel = GenericQuestionModel
            .ConvertToGenericModel<OrderingDragAndDrop>(OrderingDnDQuestionBaseModel ?? InitializeGenericQuestionModel());
        await ValueChanged();
    }

    private async Task AddNewField()
    {
        var order = OrderingDnDQuestionModel.QuestionContent.Choices.Count + 1;
        OrderingDnDQuestionModel.QuestionContent.Choices.Add(new(order)
        {
            Name = $"Item {order}",
            Selector = "0"
        });
        await ValueChanged();
    }

    private async Task CreateSample()
    {
        OrderingDnDQuestionModel = SampleHelper.GetOrderingDnDQuestionSample();
        await OrderingDnDQuestionBaseModelChanged.InvokeAsync(
            GenericQuestionModel.ConvertToNonGenericModel(OrderingDnDQuestionModel));
        await ValueChanged();
    }

    private async Task Reset()
    {
        OrderingDnDQuestionModel = InitializeGenericQuestionModel();
        await ValueChanged();
    }

    private static GenericQuestionModel<OrderingDragAndDrop> InitializeGenericQuestionModel()
    {
        return new GenericQuestionModel<OrderingDragAndDrop>(new OrderingDragAndDrop())
        {
            QuestionType = QuestionType.OrderingDragAndDrop
        };
    }

    private async Task ValueChanged()
    {
        Rerender = false;
        await Task.Delay(50);
        Rerender = true;
    }

    private async Task RemoveItem(OrderingDragAndDrop.DropItem item)
    {
        OrderingDnDQuestionModel.QuestionContent.Choices.Remove(item);
        await ValueChanged();
    }

    private async Task ItemUpdated(MudItemDropInfo<OrderingDragAndDrop.DropItem> dropItem)
    {
        if (dropItem.Item is null)
            return;

        dropItem.Item.Selector = dropItem.DropzoneIdentifier;

        var indexOffset = dropItem.DropzoneIdentifier switch
        {
            "1" => OrderingDnDQuestionModel.QuestionContent.Choices.Count(x => x.Selector == "0"),
            _ => 0,
        };
        OrderingDnDQuestionModel.QuestionContent.Choices.UpdateOrder(dropItem, item => item.Order, indexOffset);
        await OrderingDnDQuestionBaseModelChanged.InvokeAsync(
            GenericQuestionModel.ConvertToNonGenericModel(OrderingDnDQuestionModel));
    }
}