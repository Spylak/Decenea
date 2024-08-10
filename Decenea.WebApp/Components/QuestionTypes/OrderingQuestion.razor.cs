using Decenea.Common.Enums;
using Microsoft.AspNetCore.Components;
using Decenea.WebApp.Helpers;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class OrderingQuestion
{
    [Parameter]
    public GenericQuestionModel? OrderingQuestionBaseModel { get; set; }
    [Parameter] public EventCallback<GenericQuestionModel> OrderingQuestionBaseModelChanged { get; set; }
    private GenericQuestionModel<Ordering>? OrderingQuestionModel { get; set; }
    private string Choice { get; set; } = "";

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);
        if (OrderingQuestionBaseModel is not null)
        {
            OrderingQuestionModel = GenericQuestionModel.ConvertToGenericModel<Ordering>(OrderingQuestionBaseModel);
        }
    }
    
    private async Task OnClickRight(string name)
    {
        if(OrderingQuestionModel?.QuestionContent is null)
            return;
        
        var choice = OrderingQuestionModel.QuestionContent.Choices.FirstOrDefault(i => i.Text == name);
        if(choice is null)
            return;
        ListHelper.UpdateOrders(choice,OrderingQuestionModel.QuestionContent.Choices.Count - 1,OrderingQuestionModel.QuestionContent.Choices);
        choice.Active=true;
        await OrderingQuestionBaseModelChanged.InvokeAsync(GenericQuestionModel.ConvertToNonGenericModel(OrderingQuestionModel));
    } 
    private async Task OnClickLeft(string name)
    {
        if(OrderingQuestionModel?.QuestionContent is null)
            return;
        
        var choice = OrderingQuestionModel.QuestionContent.Choices.FirstOrDefault(i => i.Text == name);
        if(choice is null)
            return;
        ListHelper.UpdateOrders(choice,OrderingQuestionModel.QuestionContent.Choices.Count(i => !i.Active),OrderingQuestionModel.QuestionContent.Choices);
        choice.Active = false;
        await OrderingQuestionBaseModelChanged.InvokeAsync(GenericQuestionModel.ConvertToNonGenericModel(OrderingQuestionModel));
    }  
    private async Task OnClickUp(string name)
    {
        if(OrderingQuestionModel?.QuestionContent is null)
            return;
        
        var item = OrderingQuestionModel.QuestionContent.Choices.FirstOrDefault(i => i.Text == name);
        if(item is null)
            return;
        ListHelper.UpdateOrders(item,item.Order-1,OrderingQuestionModel.QuestionContent.Choices);
        await OrderingQuestionBaseModelChanged.InvokeAsync(GenericQuestionModel.ConvertToNonGenericModel(OrderingQuestionModel));
    }  
    private async Task OnClickDown(string name)
    {
        if(OrderingQuestionModel?.QuestionContent is null)
            return;
        
        var item = OrderingQuestionModel.QuestionContent.Choices.FirstOrDefault(i => i.Text == name);
        if(item is null)
            return;
        ListHelper.UpdateOrders(item,item.Order+1,OrderingQuestionModel.QuestionContent.Choices);
        await OrderingQuestionBaseModelChanged.InvokeAsync(GenericQuestionModel.ConvertToNonGenericModel(OrderingQuestionModel));
    }
    private async Task CreateSample()
    {
        OrderingQuestionModel = SampleHelper.GetOrderingQuestionSample();
        await OrderingQuestionBaseModelChanged.InvokeAsync(GenericQuestionModel.ConvertToNonGenericModel(OrderingQuestionModel));
    }
    private void Reset()
    {
        OrderingQuestionModel = new GenericQuestionModel<Ordering>(new Ordering())
        {
            Id = OrderingQuestionBaseModel?.Id ?? Ulid.NewUlid().ToString(),
            QuestionType = QuestionType.Ordering
        };
    }
    
    private void RemoveChoice(Ordering.Choice choice)
    {
        if(OrderingQuestionModel?.QuestionContent is null)
            return;
        
        OrderingQuestionModel.QuestionContent.Choices.Remove(choice);
    }
    
    private void AddChoice()
    {
        if (OrderingQuestionModel?.QuestionContent is null)
        {
            OrderingQuestionModel = new GenericQuestionModel<Ordering>(new Ordering())
            {
                QuestionType = QuestionType.Ordering
            };
        }
        
        var choices = OrderingQuestionModel.QuestionContent!.Choices;
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