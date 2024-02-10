using Decenea.WebApp.Helpers;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Utilities;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class OrderingDnDQuestion
{
    [Parameter]
    public QuestionBaseModel<OrderingDragAndDrop> OrderingDnDQuestionModel { get; set; } =
        new QuestionBaseModel<OrderingDragAndDrop>(new OrderingDragAndDrop());
    [Parameter] public EventCallback<QuestionBaseModel<OrderingDragAndDrop>> OrderingDnDQuestionModelChanged { get; set; }
    private bool Rerender { get; set; } = false;

    protected override async Task OnParametersSetAsync()
    {
        await ValueChanged();
    }

    private async Task AddNewField()
    {
        var order = OrderingDnDQuestionModel.QuestionContent.Choices.Count + 1;
        OrderingDnDQuestionModel.QuestionContent.Choices.Add(new (order)
        {
            Name = $"Item {order}",
            Selector = "0"
        });
        await ValueChanged();
    }
    
    private async Task CreateSample()
    {
        OrderingDnDQuestionModel = SampleHelper.GetOrderingDnDQuestionSample();
        await OrderingDnDQuestionModelChanged.InvokeAsync(OrderingDnDQuestionModel);
        await ValueChanged();
    }
    
    private async Task Reset()
    {
        OrderingDnDQuestionModel = new QuestionBaseModel<OrderingDragAndDrop>(new OrderingDragAndDrop());
        await OrderingDnDQuestionModelChanged.InvokeAsync(OrderingDnDQuestionModel);
        await ValueChanged();
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
    
    private void ItemUpdated(MudItemDropInfo<OrderingDragAndDrop.DropItem> dropItem)
    {
        dropItem.Item.Selector = dropItem.DropzoneIdentifier;
        
        var indexOffset = dropItem.DropzoneIdentifier switch
        {
            "1"  => OrderingDnDQuestionModel.QuestionContent.Choices.Count(x => x.Selector == "0"),
            _ => 0,
        };
        OrderingDnDQuestionModel.QuestionContent.Choices.UpdateOrder(dropItem, item => item.Order, indexOffset);
    }
}