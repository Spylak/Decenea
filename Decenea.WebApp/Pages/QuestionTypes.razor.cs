using Decenea.Common.Enums;
using Decenea.WebApp.Abstractions;
using Microsoft.AspNetCore.Components;

using Decenea.WebApp.Helpers;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Pages;

public partial class QuestionTypes
{
    [Inject] private IGlobalFunctionService GlobalFunctionService { get; set; }
    int activeIndex = 0;

    private GenericQuestionModel DragAndDrop { get; set; } = GenericQuestionModel.ConvertToGenericModel<DragAndDrop>(SampleHelper.GetDragAndDropQuestionSample());
    
    private GenericQuestionModel OrderingDnDGenericQuestionModel { get; set; } = GenericQuestionModel.ConvertToGenericModel<OrderingDragAndDrop>(SampleHelper.GetOrderingDnDQuestionSample());
    
    private GenericQuestionModel FillBlank { get; set; } = GenericQuestionModel.ConvertToGenericModel<FillBlank>(SampleHelper.GetFillBlankQuestionSample());

    private GenericQuestionModel OrderingGenericQuestionModel { get; set; } = GenericQuestionModel.ConvertToGenericModel<Ordering>(SampleHelper.GetOrderingQuestionSample());

    private GenericQuestionModel MultipleYesOrNo { get; set; } = GenericQuestionModel.ConvertToGenericModel<MultipleYesOrNo>(SampleHelper.GetMultipleYesOrNoQuestionSample());

    private GenericQuestionModel MultipleChoice { get; set; } = GenericQuestionModel.ConvertToGenericModel<MultipleChoice>(SampleHelper.GetMultipleChoiceQuestionSample());

    private GenericQuestionModel Dropdown { get; set; } = GenericQuestionModel.ConvertToGenericModel<Dropdown>(SampleHelper.GetDropdownQuestionSample());

    private GenericQuestionModel MultipleChoiceSingle { get; set; } = GenericQuestionModel.ConvertToGenericModel<MultipleChoiceSingle>(SampleHelper.GetMultipleChoiceSingleQuestionSample());
    private GenericQuestionModel FillBlankDropdownGenericQuestion { get; set; } = GenericQuestionModel.ConvertToGenericModel<FillBlankDropdown>(SampleHelper.GetFillBlankDropdownQuestionSample());

    private Dictionary<string, bool> Visibility = new Dictionary<string, bool>()
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