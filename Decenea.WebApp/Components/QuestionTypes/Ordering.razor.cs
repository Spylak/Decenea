using Microsoft.AspNetCore.Components;
using Decenea.WebApp.Helpers;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class Ordering
{
    [Parameter] public OrderingQuestionModel OrderingQuestionModel { get; set; } = new OrderingQuestionModel();
    [Parameter] public EventCallback<OrderingQuestionModel> OrderingQuestionModelChanged { get; set; }
    private string Choice { get; set; } = "";
    
    private void OnClickRight(string name)
    {
        var choice = OrderingQuestionModel.Choices.FirstOrDefault(i => i.Text == name);
        if(choice is null)
            return;
        ListHelper.UpdateOrders(choice,OrderingQuestionModel.Choices.Count - 1,OrderingQuestionModel.Choices);
        choice.Active=true;
    } 
    private void OnClickLeft(string name)
    {
        var choice = OrderingQuestionModel.Choices.FirstOrDefault(i => i.Text == name);
        if(choice is null)
            return;
        ListHelper.UpdateOrders(choice,OrderingQuestionModel.Choices.Where(i => !i.Active).Count(),OrderingQuestionModel.Choices);
        choice.Active = false;
    }  
    private void OnClickUp(string name)
    {
        var item = OrderingQuestionModel.Choices.FirstOrDefault(i => i.Text == name);
        if(item is null)
            return;
        ListHelper.UpdateOrders(item,item.Order-1,OrderingQuestionModel.Choices);
    }  
    private void OnClickDown(string name)
    {
        var item = OrderingQuestionModel.Choices.FirstOrDefault(i => i.Text == name);
        if(item is null)
            return;
        ListHelper.UpdateOrders(item,item.Order+1,OrderingQuestionModel.Choices);
    }
    private async Task CreateSample()
    {
        OrderingQuestionModel = SampleService.GetOrderingQuestionSample();
        await OrderingQuestionModelChanged.InvokeAsync(OrderingQuestionModel);
    }
    private async Task Reset()
    {
        OrderingQuestionModel = new OrderingQuestionModel();
        await OrderingQuestionModelChanged.InvokeAsync(OrderingQuestionModel);
    }
    
    private void RemoveChoice(OrderingQuestionModel.Choice choice)
    {
        OrderingQuestionModel.Choices.Remove(choice);
    }
    
    private void AddChoice()
    {
        var choices = OrderingQuestionModel.Choices;
        if (choices.Select(i => i.Text).Contains(Choice))
        {
            // There is already a choice like this
            return;
        }

        if (string.IsNullOrWhiteSpace(Choice))
        {
            // Choice is empty
            return;
        }
        
        OrderingQuestionModel.Choices.Add(new OrderingQuestionModel.Choice()
        {
            Text = Choice,
            Order = choices.Count,
            Active = false
        });
        Choice = "";
    }
}