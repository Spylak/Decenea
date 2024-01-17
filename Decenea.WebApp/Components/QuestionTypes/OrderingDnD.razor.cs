using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Utilities;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class OrderingDnD
{
    [Parameter] public OrderingDnDQuestionModel OrderingDnDQuestionModel { get; set; } = new OrderingDnDQuestionModel();
    [Parameter] public EventCallback<OrderingDnDQuestionModel> OrderingDnDQuestionModelChanged { get; set; }
    private bool Rerender { get; set; } = false;

    protected override async Task OnParametersSetAsync()
    {
        await ValueChanged();
    }

    private async Task AddNewField()
    {
        var order = OrderingDnDQuestionModel.Choices.Count + 1;
        OrderingDnDQuestionModel.Choices.Add(new OrderingDnDQuestionModel.DropItem(order)
        {
            Name = $"Item {order}",
            Selector = "0"
        });
        await ValueChanged();
    }
    
    private async Task CreateSample()
    {
        OrderingDnDQuestionModel = SampleService.GetOrderingDnDQuestionSample();
        await OrderingDnDQuestionModelChanged.InvokeAsync(OrderingDnDQuestionModel);
        await ValueChanged();
    }
    
    private async Task Reset()
    {
        OrderingDnDQuestionModel = new OrderingDnDQuestionModel();
        await OrderingDnDQuestionModelChanged.InvokeAsync(OrderingDnDQuestionModel);
        await ValueChanged();
    }

    private async Task ValueChanged()
    {
        Rerender = false;
        await Task.Delay(50);
        Rerender = true;
    }

    private async Task RemoveItem(OrderingDnDQuestionModel.DropItem item)
    {
        OrderingDnDQuestionModel.Choices.Remove(item);
        await ValueChanged();
    }
    
    private void ItemUpdated(MudItemDropInfo<OrderingDnDQuestionModel.DropItem> dropItem)
    {
        dropItem.Item.Selector = dropItem.DropzoneIdentifier;
        
        var indexOffset = dropItem.DropzoneIdentifier switch
        {
            "1"  => OrderingDnDQuestionModel.Choices.Count(x => x.Selector == "0"),
            _ => 0,
        };
        OrderingDnDQuestionModel.Choices.UpdateOrder(dropItem, item => item.Order, indexOffset);
    }
}