using Microsoft.AspNetCore.Components;
using Decenea.Common.Constants;
using Decenea.WebApp.Helpers;
using Decenea.WebApp.Models.QuestionTypes;
using Decenea.WebApp.Services.IService;

namespace Decenea.WebApp.Pages;

public partial class QuestionTypes
{
    [Inject] private IGlobalFunctionService GlobalFunctionService { get; set; }
    int activeIndex = 0;

    private QuestionBaseModel DragAndDrop { get; set; } = QuestionBaseModel.ConvertToGenericBaseModel<DragAndDrop>(SampleHelper.GetDragAndDropQuestionSample());
    
    private QuestionBaseModel OrderingDnDQuestionModel { get; set; } = QuestionBaseModel.ConvertToGenericBaseModel<OrderingDragAndDrop>(SampleHelper.GetOrderingDnDQuestionSample());
    
    private QuestionBaseModel FillBlank { get; set; } = QuestionBaseModel.ConvertToGenericBaseModel<FillBlank>(SampleHelper.GetFillBlankQuestionSample());

    private QuestionBaseModel OrderingQuestionModel { get; set; } = QuestionBaseModel.ConvertToGenericBaseModel<Ordering>(SampleHelper.GetOrderingQuestionSample());

    private QuestionBaseModel MultipleYesOrNo { get; set; } = QuestionBaseModel.ConvertToGenericBaseModel<MultipleYesOrNo>(SampleHelper.GetMultipleYesOrNoQuestionSample());

    private QuestionBaseModel MultipleChoice { get; set; } = QuestionBaseModel.ConvertToGenericBaseModel<MultipleChoice>(SampleHelper.GetMultipleChoiceQuestionSample());

    private QuestionBaseModel Dropdown { get; set; } = QuestionBaseModel.ConvertToGenericBaseModel<Dropdown>(SampleHelper.GetDropdownQuestionSample());

    private QuestionBaseModel MultipleChoiceSingle { get; set; } = QuestionBaseModel.ConvertToGenericBaseModel<MultipleChoiceSingle>(SampleHelper.GetMultipleChoiceSingleQuestionSample());
    private QuestionBaseModel FillBlankDropdownQuestion { get; set; } = QuestionBaseModel.ConvertToGenericBaseModel<FillBlankDropdown>(SampleHelper.GetFillBlankDropdownQuestionSample());

    private Dictionary<string, bool> Visibility = new Dictionary<string, bool>()
    {
        { QuestionTypeValues.DragAndDrop, true },
        { QuestionTypeValues.OrderingDragAndDrop, false },
        { QuestionTypeValues.Dropdown, false },
        { QuestionTypeValues.FillBlank, false },
        { QuestionTypeValues.FillBlankDropdown, false },
        { QuestionTypeValues.MultipleChoice, false },
        { QuestionTypeValues.MultipleChoiceSingle, false },
        { QuestionTypeValues.MultipleYesOrNo, false },
        { QuestionTypeValues.Ordering, false }
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