using Decenea.Common.Enums;
using Decenea.WebApp.Abstractions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

using Decenea.WebApp.Constants;
using Decenea.WebApp.Models;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class QuestionTypesComponent
{
    [Inject] private IGlobalFunctionService GlobalFunctionService { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }
    [Parameter] public TestModel TestModel { get; set; }
    [Parameter] public GenericQuestionModel? Question { get; set; }
    [Parameter] public EventCallback<GenericQuestionModel> QuestionChanged { get; set; }
    [Parameter] public string Style { get; set; } = "";

    [Parameter] public bool PresentationOnly { get; set; } = false;
    private string VisibleQuestionType { get; set; } = nameof(QuestionType.MultipleChoiceSingle);
    private bool EditMode { get; set; } = false;
    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);
        if (Question is not null)
        {
            ChangeType(Question.QuestionType.ToString());
        }
    }

    private void ChangeType(string? type = null)
    {
        if (type is null)
            return;
        
        VisibleQuestionType = type;
        StateHasChanged();
    }
    private void SaveQuestionToTest(GenericQuestionModel genericQuestionModel)
    {
        var question = TestModel.GenericQuestionModels
            .FirstOrDefault(i => i.Id.Equals(genericQuestionModel.Id));
        if (question is null)
        {
            if (!string.IsNullOrWhiteSpace(genericQuestionModel.Description))
            {
                TestModel.GenericQuestionModels.Add(genericQuestionModel);
                Snackbar.Add(Messages.QuestionSaved, Severity.Success);
                return;
            }
        }
        else
        {
            TestModel.GenericQuestionModels.Remove(question);
            TestModel.GenericQuestionModels.Add(genericQuestionModel);
            Snackbar.Add(Messages.QuestionSaved, Severity.Success);
            return;
        }

        Snackbar.Add(Messages.QuestionError, Severity.Error);
    }

    private async Task UpdateQuestion(GenericQuestionModel genericQuestionModel)
    {
        var question = TestModel.GenericQuestionModels
            .FirstOrDefault(i => i.Id.Equals(genericQuestionModel.Id));
        if (question is null)
            return;
        TestModel.GenericQuestionModels.Remove(question);
        TestModel.GenericQuestionModels.Add(genericQuestionModel);
        await QuestionChanged.InvokeAsync(genericQuestionModel);
    }
}