using Microsoft.AspNetCore.Components;
using Decenea.WebApp.Helpers;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class OrderingQuestion
{
    [Parameter]
    public QuestionBaseModel<Ordering> OrderingQuestionModel { get; set; } = new QuestionBaseModel<Ordering>(new Ordering());
    [Parameter] public EventCallback<QuestionBaseModel<Ordering>> OrderingQuestionModelChanged { get; set; }
    private string Choice { get; set; } = "";
    
    private void OnClickRight(string name)
    {
        var choice = OrderingQuestionModel.QuestionContent.Choices.FirstOrDefault(i => i.Text == name);
        if(choice is null)
            return;
        ListHelper.UpdateOrders(choice,OrderingQuestionModel.QuestionContent.Choices.Count - 1,OrderingQuestionModel.QuestionContent.Choices);
        choice.Active=true;
    } 
    private void OnClickLeft(string name)
    {
        var choice = OrderingQuestionModel.QuestionContent.Choices.FirstOrDefault(i => i.Text == name);
        if(choice is null)
            return;
        ListHelper.UpdateOrders(choice,OrderingQuestionModel.QuestionContent.Choices.Count(i => !i.Active),OrderingQuestionModel.QuestionContent.Choices);
        choice.Active = false;
    }  
    private void OnClickUp(string name)
    {
        var item = OrderingQuestionModel.QuestionContent.Choices.FirstOrDefault(i => i.Text == name);
        if(item is null)
            return;
        ListHelper.UpdateOrders(item,item.Order-1,OrderingQuestionModel.QuestionContent.Choices);
    }  
    private void OnClickDown(string name)
    {
        var item = OrderingQuestionModel.QuestionContent.Choices.FirstOrDefault(i => i.Text == name);
        if(item is null)
            return;
        ListHelper.UpdateOrders(item,item.Order+1,OrderingQuestionModel.QuestionContent.Choices);
    }
    private async Task CreateSample()
    {
        OrderingQuestionModel = SampleHelper.GetOrderingQuestionSample();
        await OrderingQuestionModelChanged.InvokeAsync(OrderingQuestionModel);
    }
    private async Task Reset()
    {
        OrderingQuestionModel = new QuestionBaseModel<Ordering>(new Ordering());
        await OrderingQuestionModelChanged.InvokeAsync(OrderingQuestionModel);
    }
    
    private void RemoveChoice(Ordering.Choice choice)
    {
        OrderingQuestionModel.QuestionContent.Choices.Remove(choice);
    }
    
    private void AddChoice()
    {
        var choices = OrderingQuestionModel.QuestionContent.Choices;
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
        
        OrderingQuestionModel.QuestionContent.Choices.Add(new Ordering.Choice()
        {
            Text = Choice,
            Order = choices.Count,
            Active = false
        });
        Choice = "";
    }
}