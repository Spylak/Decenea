using Decenea.WebApp.Models.QuestionTypes;
using Microsoft.AspNetCore.Components;
using Decenea.WebApp.Services.IService;

namespace Decenea.WebApp.Components.QuestionTypes;

public class QuestionBaseComponent : ComponentBase
{
    [Parameter] public EventCallback<GenericQuestionModel> SaveCallback { get; set; }
    // [Parameter] public bool ReadOnly { get; set; } = false;
    [Parameter] public bool EditMode { get; set; } = false;
    [Inject] protected IGlobalFunctionService GlobalFunctionService { get; set; }
    protected async Task Save(GenericQuestionModel genericQuestionModel)
    {
        await SaveCallback.InvokeAsync(genericQuestionModel);
    }
}