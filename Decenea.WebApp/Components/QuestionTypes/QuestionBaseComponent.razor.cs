using Decenea.Common.DataTransferObjects.Question.QuestionTypes;
using Microsoft.AspNetCore.Components;

namespace Decenea.WebApp.Components.QuestionTypes;

public class QuestionBaseComponent : BaseComponent
{
    [Parameter] public EventCallback<GenericQuestionModel> SaveCallback { get; set; }
    [Parameter] public bool EditMode { get; set; } = false;
    protected async Task Save(GenericQuestionModel genericQuestionModel)
    {
        await SaveCallback.InvokeAsync(genericQuestionModel);
    }
}