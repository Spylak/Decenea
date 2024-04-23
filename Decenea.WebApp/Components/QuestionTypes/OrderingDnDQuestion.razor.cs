using Decenea.WebApp.Helpers;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Utilities;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class OrderingDnDQuestion
{
    [Parameter]
    public QuestionBaseModel? OrderingDnDQuestionBaseModel { get; set; }
    [Parameter] public EventCallback<QuestionBaseModel> OrderingDnDQuestionBaseModelChanged { get; set; }
    private QuestionBaseModel<OrderingDragAndDrop>? OrderingDnDQuestionModel { get; set; }

    private bool Rerender { get; set; } = false;
    
    protected override async Task OnParametersSetAsync()
    {
        if (OrderingDnDQuestionBaseModel is not null)
        {
            OrderingDnDQuestionModel = QuestionBaseModel.ConvertToGenericBaseModel<OrderingDragAndDrop>(OrderingDnDQuestionBaseModel);
        }
        await ValueChanged();
    }

    private async Task AddNewField()
    {
        if(OrderingDnDQuestionModel?.QuestionContent is null)
            return;
        
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
        await OrderingDnDQuestionBaseModelChanged.InvokeAsync(QuestionBaseModel.ConvertToNonGenericBaseModel(OrderingDnDQuestionModel));
        await ValueChanged();
    }
    
    private async Task Reset()
    {
        OrderingDnDQuestionModel = new QuestionBaseModel<OrderingDragAndDrop>(new OrderingDragAndDrop());
        OrderingDnDQuestionModel.Id = OrderingDnDQuestionBaseModel?.Id ?? Guid.NewGuid().ToString();
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
        if(OrderingDnDQuestionModel?.QuestionContent is null)
            return;
        
        OrderingDnDQuestionModel.QuestionContent.Choices.Remove(item);
        await ValueChanged();
    }
    
    private async Task ItemUpdated(MudItemDropInfo<OrderingDragAndDrop.DropItem> dropItem)
    {
        if(OrderingDnDQuestionModel?.QuestionContent is null || dropItem.Item is null)
            return;
        
        dropItem.Item.Selector = dropItem.DropzoneIdentifier;
        
        var indexOffset = dropItem.DropzoneIdentifier switch
        {
            "1"  => OrderingDnDQuestionModel.QuestionContent.Choices.Count(x => x.Selector == "0"),
            _ => 0,
        };
        OrderingDnDQuestionModel.QuestionContent.Choices.UpdateOrder(dropItem, item => item.Order, indexOffset);
        await OrderingDnDQuestionBaseModelChanged.InvokeAsync(QuestionBaseModel.ConvertToNonGenericBaseModel(OrderingDnDQuestionModel));
    }
}