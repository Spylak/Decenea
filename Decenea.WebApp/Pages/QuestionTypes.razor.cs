using Decenea.Common.DataTransferObjects.Question.QuestionTypes;
using Decenea.Common.Enums;
using Decenea.WebApp.Abstractions;
using Microsoft.AspNetCore.Components;
using Decenea.WebApp.State;

namespace Decenea.WebApp.Pages;

public partial class QuestionTypes
{
    [Inject] private IGlobalFunctionService GlobalFunctionService { get; set; } = null!;
    [Inject] private QuestionTypesContainer QuestionTypesContainer { get; set; } = null!;
    int activeIndex = 0;

    private void ValueChanged(GenericQuestionModel newValue)
    {
        QuestionTypesContainer.MultipleChoiceSingle = newValue;
    }
    private Dictionary<string, bool> Visibility = new ()
    {
        { nameof(QuestionType.DragAndDrop), true },
        { nameof(QuestionType.OrderingDragAndDrop), false },
        { nameof(QuestionType.Dropdown), false },
        { nameof(QuestionType.FillBlank), false },
        { nameof(QuestionType.FillBlankDropdown), false },
        { nameof(QuestionType.MultipleChoice), false },
        { nameof(QuestionType.MultipleChoiceSingle), false },
        { nameof(QuestionType.MultipleYesOrNo), false },
        { nameof(QuestionType.Ordering), false }
    };

    private void OpenOverlay(string type)
    {
        foreach (var item in Visibility.Keys)
        {
            Visibility[item] = false;
        }

        Visibility[type] = true;
        StateHasChanged();
    }
}