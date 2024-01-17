using Microsoft.AspNetCore.Components;
using Decenea.WebApp.Services.IService;

namespace Decenea.WebApp.Components.QuestionTypes;

public class QuestionBaseComponent : ComponentBase
{
    [Parameter] public EventCallback SaveCallback { get; set; }
    [Parameter] public bool ReadOnly { get; set; } = false;
    [Inject] protected IGlobalFunctionService GlobalFunctionService { get; set; }
    [Inject] protected ISampleService SampleService { get; set; }
    protected async Task Save()
    {
        await SaveCallback.InvokeAsync();
    }
}