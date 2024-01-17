using Microsoft.AspNetCore.Components;
using MudBlazor;
using Decenea.Common.Constants;
using Decenea.WebApp.Constants;
using Decenea.WebApp.Models;
using Decenea.WebApp.Models.QuestionTypes;
using Decenea.WebApp.Services.IService;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class QuestionTypesComponent
{
    [Inject] private IGlobalFunctionService GlobalFunctionService { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }
    [Parameter] public Test Test { get; set; }
    [Parameter] public object? Question { get; set; }
    [Parameter] public string Style { get; set; } = "";

    [Parameter] public bool PresentationOnly { get; set; } = false;
    [Parameter] public DragAndDropQuestionModel DragAndDrop { get; set; } = new DragAndDropQuestionModel();
    [Parameter] public OrderingDnDQuestionModel OrderingDnD { get; set; } = new OrderingDnDQuestionModel();
    [Parameter] public FillBlankQuestionModel FillBlank { get; set; } = new FillBlankQuestionModel();
    [Parameter] public OrderingQuestionModel Ordering { get; set; } = new OrderingQuestionModel();
    [Parameter] public MultipleYesOrNoQuestionModel MultipleYesOrNo { get; set; } = new MultipleYesOrNoQuestionModel();
    [Parameter] public MultipleChoiceQuestionModel MultipleChoice { get; set; } = new MultipleChoiceQuestionModel();
    [Parameter] public DropdownQuestionModel Dropdown { get; set; } = new DropdownQuestionModel();

    [Parameter]
    public MultipleChoiceSingleQuestionModel MultipleChoiceSingle { get; set; } = new MultipleChoiceSingleQuestionModel();

    [Parameter] public FillBlankDropdownQuestionModel FillBlankDropdown { get; set; } = new FillBlankDropdownQuestionModel();
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

    private void ChangeType(string? type = null)
    {
        if (type is null)
            return;
        VisibleQuestionType = type;
    }
    private void SaveOrderingDnDQuestionToTest()
    {
        var question = Test.OrderingDnDQuestions
            .FirstOrDefault(i => i.Id!.Equals(OrderingDnD.Id));
        if (question is null)
        {
            if (!string.IsNullOrWhiteSpace(OrderingDnD.Question))
            {
                Test.OrderingDnDQuestions.Add(OrderingDnD);
                OrderingDnD = new OrderingDnDQuestionModel();
                Snackbar.Add(Messages.QuestionSaved, Severity.Success);
                return;
            }
        }
        else
        {
                question = OrderingDnD;
                Snackbar.Add(Messages.QuestionSaved, Severity.Success);
                return;
        }

        Snackbar.Add(Messages.QuestionError, Severity.Error);
    }

    private void SaveMultipleChoiceSingleQuestionToTest()
    {
        var question = Test.MultipleChoiceSingleQuestions
            .FirstOrDefault(i => i.Id!.Equals(MultipleChoiceSingle.Id));
        if (question is null)
        {
            if (!string.IsNullOrWhiteSpace(MultipleChoiceSingle.Question))
            {
                Test.MultipleChoiceSingleQuestions.Add(MultipleChoiceSingle);
                MultipleChoiceSingle = new MultipleChoiceSingleQuestionModel();
                Snackbar.Add(Messages.QuestionSaved, Severity.Success);
                return;
            }
        }
        else
        {
                question = MultipleChoiceSingle;
                Snackbar.Add(Messages.QuestionSaved, Severity.Success);
                return;
        }

        Snackbar.Add(Messages.QuestionError, Severity.Error);
    }

    private void SaveOrderingQuestionToTest()
    {
        var question = Test.OrderingQuestions
            .FirstOrDefault(i => i.Id!.Equals(Ordering.Id));
        if (question is null)
        {
            if (!string.IsNullOrWhiteSpace(Ordering.Question))
            {
                Test.OrderingQuestions.Add(Ordering);
                Ordering = new OrderingQuestionModel();
                Snackbar.Add(Messages.QuestionSaved, Severity.Success);
                return;
            }
        }
        else
        {
            question = Ordering;
            Snackbar.Add(Messages.QuestionSaved, Severity.Success);
            return;
        }

        Snackbar.Add(Messages.QuestionError, Severity.Error);
    }

    private void SaveMultipleYesOrNoQuestionToTest()
    {
        var question = Test.MultipleYesOrNoQuestions
            .FirstOrDefault(i => i.Id!.Equals(MultipleYesOrNo.Id));
        if (question is null)
        {
            if (!string.IsNullOrWhiteSpace(MultipleYesOrNo.Question))
            {
                Test.MultipleYesOrNoQuestions.Add(MultipleYesOrNo);
                MultipleYesOrNo = new MultipleYesOrNoQuestionModel();
                Snackbar.Add(Messages.QuestionSaved, Severity.Success);
                return;
            }
        }
        else
        {
                question = MultipleYesOrNo;
                Snackbar.Add(Messages.QuestionSaved, Severity.Success);
                return;
        }

        Snackbar.Add(Messages.QuestionError, Severity.Error);
    }

    private void SaveMultipleChoiceQuestionToTest()
    {
        var question = Test.MultipleChoiceQuestions
            .FirstOrDefault(i => i.Id!.Equals(MultipleChoice.Id));
        if (question is null)
        {
            if (!string.IsNullOrWhiteSpace(MultipleChoice.Question))
            {
                Test.MultipleChoiceQuestions.Add(MultipleChoice);
                MultipleChoice = new MultipleChoiceQuestionModel();
                Snackbar.Add(Messages.QuestionSaved, Severity.Success);
                return;
            }
        }
        else
        {
                question = MultipleChoice;
                Snackbar.Add(Messages.QuestionSaved, Severity.Success);
                return;
        }

        Snackbar.Add(Messages.QuestionError, Severity.Error);
    }

    private void SaveDropdownQuestionToTest()
    {
        var question = Test.DropdownQuestions
            .FirstOrDefault(i => i.Id!.Equals(Dropdown.Id));
        if (question is null)
        {
            if (!string.IsNullOrWhiteSpace(Dropdown.Question))
            {
                Test.DropdownQuestions.Add(Dropdown);
                Dropdown = new DropdownQuestionModel();
                Snackbar.Add(Messages.QuestionSaved, Severity.Success);
                return;
            }
        }
        else
        {
                question = Dropdown;
                Snackbar.Add(Messages.QuestionSaved, Severity.Success);
                return;
        }

        Snackbar.Add(Messages.QuestionError, Severity.Error);
    }

    private void SaveDragAndDropQuestionToTest()
    {
        var question = Test.DragAndDropQuestions
            .FirstOrDefault(i => i.Id!.Equals(DragAndDrop.Id));
        if (question is null)
        {
            if (!string.IsNullOrWhiteSpace(DragAndDrop.Question))
            {
                Test.DragAndDropQuestions.Add(DragAndDrop);
                DragAndDrop = new DragAndDropQuestionModel();
                Snackbar.Add(Messages.QuestionSaved, Severity.Success);
                return;
            }
        }
        else
        {
                question = DragAndDrop;
                Snackbar.Add(Messages.QuestionSaved, Severity.Success);
                return;
        }

        Snackbar.Add(Messages.QuestionError, Severity.Error);
    }

    private void SaveInTextDropdownQuestionToTest()
    {
        var question = Test.FillblankDropdownQuestions
            .FirstOrDefault(i => i.Id!.Equals(FillBlankDropdown.Id));
        if (question is null)
        {
            if (!string.IsNullOrWhiteSpace(FillBlankDropdown.Question))
            {
                Test.FillblankDropdownQuestions.Add(FillBlankDropdown);
                FillBlankDropdown = new FillBlankDropdownQuestionModel();
                Snackbar.Add(Messages.QuestionSaved, Severity.Success);
                return;
            }
        }
        else
        {
                question = FillBlankDropdown;
                Snackbar.Add(Messages.QuestionSaved, Severity.Success);
                return;
        }

        Snackbar.Add(Messages.QuestionError, Severity.Error);
    }

    private void SaveFillBlankQuestionToTest()
    {
        var question = Test.FillBlankQuestions
            .FirstOrDefault(i => i.Id!.Equals(FillBlank.Id));
        if (question is null)
        {
            if (!string.IsNullOrWhiteSpace(FillBlank.Question))
            {
                Test.FillBlankQuestions.Add(FillBlank);
                FillBlank = new FillBlankQuestionModel();
                Snackbar.Add(Messages.QuestionSaved, Severity.Success);
                return;
            }
        }
        else
        {
                question = FillBlank;
                Snackbar.Add(Messages.QuestionSaved, Severity.Success);
                return;
        }

        Snackbar.Add(Messages.QuestionError, Severity.Error);
    }

    private async Task PopulateDialog()
    {
        var type = Question.GetType().GetProperty("QuestionType").GetValue(Question).ToString();
        switch (type)
        {
            case QuestionTypeValues.Dropdown:
                Dropdown = Question as DropdownQuestionModel;
                break;
            case QuestionTypeValues.Ordering:
                Ordering = Question as OrderingQuestionModel;
                break;
            case QuestionTypeValues.Fillblank:
                FillBlank = Question as FillBlankQuestionModel;
                break;
            case QuestionTypeValues.MultipleChoice:
                MultipleChoice = Question as MultipleChoiceQuestionModel;
                break;
            case QuestionTypeValues.DragAndDrop:
                DragAndDrop = Question as DragAndDropQuestionModel;
                break;
            case QuestionTypeValues.FillblankDropdown:
                FillBlankDropdown = Question as FillBlankDropdownQuestionModel;
                break;
            case QuestionTypeValues.MultipleChoiceSingle:
                MultipleChoiceSingle = Question as MultipleChoiceSingleQuestionModel;
                break;
            case QuestionTypeValues.OrderingDragAndDrop:
                OrderingDnD = Question as OrderingDnDQuestionModel;
                break;
            case QuestionTypeValues.MultipleYesOrNo:
                MultipleYesOrNo = Question as MultipleYesOrNoQuestionModel;
                break;
            default:
                break;
        }

        await Task.Delay(50);
        ChangeType(type);
        StateHasChanged();
    }
}