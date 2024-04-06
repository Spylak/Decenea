using Microsoft.AspNetCore.Components;
using MudBlazor;
using Decenea.Common.Constants;
using Decenea.WebApp.Constants;
using Decenea.WebApp.Helpers;
using Decenea.WebApp.Models;
using Decenea.WebApp.Models.QuestionTypes;
using Decenea.WebApp.Services.IService;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class QuestionTypesComponent
{
    [Inject] private IGlobalFunctionService GlobalFunctionService { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }
    [Parameter] public Test Test { get; set; }

    [Parameter] public QuestionBaseModel? Question { get; set; }
    [Parameter] public string Style { get; set; } = "";

    [Parameter] public bool PresentationOnly { get; set; } = false;
    private string VisibleQuestionType { get; set; } = QuestionTypeValues.MultipleChoiceSingle;
    private bool PreviewMode { get; set; } = false;
    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);
        if (Question is not null)
        {
            ChangeType(Question.QuestionType);
        }
    }

    private void ChangeType(string? type = null)
    {
        if (type is null)
            return;
        VisibleQuestionType = type;
        StateHasChanged();
    }
    private void SaveQuestionToTest(QuestionBaseModel questionBaseModel)
    {
        var question = Test.QuestionBaseModels
            .FirstOrDefault(i => i.Id!.Equals(questionBaseModel.Id));
        if (question is null)
        {
            if (!string.IsNullOrWhiteSpace(questionBaseModel.Description))
            {
                Test.QuestionBaseModels.Add(questionBaseModel);
                Question = new QuestionBaseModel(QuestionTypeValues.MultipleChoiceSingle);
                Snackbar.Add(Messages.QuestionSaved, Severity.Success);
                return;
            }
        }
        else
        {
                Snackbar.Add(Messages.QuestionSaved, Severity.Success);
                return;
        }

        Snackbar.Add(Messages.QuestionError, Severity.Error);
    }
}