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

    [Parameter]
    public QuestionBaseModel<FillBlank>? FillBlankModel { get; set; } =
        new QuestionBaseModel<FillBlank>(new FillBlank());

    [Parameter]
    public QuestionBaseModel<Ordering>? OrderingModel { get; set; } = new QuestionBaseModel<Ordering>(new Ordering());

    [Parameter]
    public QuestionBaseModel<OrderingDragAndDrop>? OrderingDragAndDropModel { get; set; } =
        new QuestionBaseModel<OrderingDragAndDrop>(new OrderingDragAndDrop());

    [Parameter]
    public QuestionBaseModel<FillBlankDropdown>? FillBlankDropdownModel { get; set; } =
        new QuestionBaseModel<FillBlankDropdown>(new FillBlankDropdown());

    [Parameter]
    public QuestionBaseModel<Dropdown>? DropdownModel { get; set; } = new QuestionBaseModel<Dropdown>(new Dropdown());

    [Parameter]
    public QuestionBaseModel<DragAndDrop>? DragAndDropModel { get; set; } =
        new QuestionBaseModel<DragAndDrop>(new DragAndDrop());

    [Parameter]
    public QuestionBaseModel<MultipleChoice>? MultipleChoiceModel { get; set; } =
        new QuestionBaseModel<MultipleChoice>(new MultipleChoice());

    [Parameter]
    public QuestionBaseModel<MultipleYesOrNo>? MultipleYesOrNoModel { get; set; } =
        new QuestionBaseModel<MultipleYesOrNo>(new MultipleYesOrNo());

    [Parameter]
    public QuestionBaseModel<MultipleChoiceSingle>? MultipleChoiceSingleModel { get; set; } =
        new QuestionBaseModel<MultipleChoiceSingle>(new MultipleChoiceSingle());
    private string VisibleQuestionType { get; set; } = QuestionTypeValues.MultipleChoiceSingle;
    private bool PreviewMode { get; set; } = false;
    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);
        if (Question is not null)
        {
            await PopulateDialog();
        }
    }

    protected override void OnInitialized()
    {
        Console.WriteLine(VisibleQuestionType);
        base.OnInitialized();
    }

    private void ChangeType(string? type = null)
    {
        if (type is null)
            return;
        VisibleQuestionType = type;
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
                Question = new QuestionBaseModel<MultipleChoiceSingle>(new MultipleChoiceSingle());
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

    private async Task PopulateDialog()
    {
        var type = Question?.GetType().GetProperty("QuestionType")?.GetValue(Question)?.ToString();
        switch (type)
        {
            case QuestionTypeValues.Dropdown:
                DropdownModel = Question as QuestionBaseModel<Dropdown>;
                break;
            case QuestionTypeValues.Ordering:
                OrderingModel = Question as QuestionBaseModel<Ordering>;
                break;
            case QuestionTypeValues.FillBlank:
                FillBlankModel = Question as QuestionBaseModel<FillBlank>;
                break;
            case QuestionTypeValues.MultipleChoice:
                MultipleChoiceModel = Question as QuestionBaseModel<MultipleChoice>;
                break;
            case QuestionTypeValues.DragAndDrop:
                DragAndDropModel = Question as QuestionBaseModel<DragAndDrop>;
                break;
            case QuestionTypeValues.FillBlankDropdown:
                FillBlankDropdownModel = Question as QuestionBaseModel<FillBlankDropdown>;
                break;
            case QuestionTypeValues.MultipleChoiceSingle:
                MultipleChoiceSingleModel = Question as QuestionBaseModel<MultipleChoiceSingle>;
                break;
            case QuestionTypeValues.OrderingDragAndDrop:
                OrderingDragAndDropModel = Question as QuestionBaseModel<OrderingDragAndDrop>;
                break;
            case QuestionTypeValues.MultipleYesOrNo:
                MultipleYesOrNoModel = Question as QuestionBaseModel<MultipleYesOrNo>;
                break;
            default:
                break;
        }

        await Task.Delay(50);
        ChangeType(type);
        StateHasChanged();
    }
}