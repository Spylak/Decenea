using Decenea.Common.DataTransferObjects.Question.QuestionTypes;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Decenea.WebApp.Models;

namespace Decenea.WebApp.Components;

public partial class QuestionTypesDialog
{
    [CascadingParameter] MudDialogInstance? MudDialog { get; set; }

    [Parameter] public string? ContentText { get; set; }
    [Parameter] public TestModel? TestModel { get; set; }
    [Parameter] public GenericQuestionModel? GenericQuestion { get; set; }

    [Parameter] public string? ButtonText { get; set; }

    [Parameter] public Color Color { get; set; }
    protected override void OnInitialized()
    {
        if (TestModel is null && GenericQuestion is null)
            throw new ApplicationException("You must provide Test and Qeustion");
        base.OnInitialized();
    }

    private void Submit()
    {
        MudDialog!.Close(DialogResult.Ok(true));
    }

    private void Cancel() => MudDialog!.Cancel();
}