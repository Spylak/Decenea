using Microsoft.AspNetCore.Components;
using Decenea.Common.Constants;
using Decenea.WebApp.Helpers;
using Decenea.WebApp.Models.QuestionTypes;
using Decenea.WebApp.Services;
using Decenea.WebApp.Services.IService;

namespace Decenea.WebApp.Pages;

public partial class QuestionTypes
{
    int activeIndex = 0;

    private QuestionBaseModel<DragAndDrop> DragAndDrop;
    
    private QuestionBaseModel<OrderingDragAndDrop> OrderingDnDQuestionModel;
    
    private QuestionBaseModel<FillBlank> FillBlank;

    private QuestionBaseModel<Ordering> OrderingQuestionModel;

    private QuestionBaseModel<MultipleYesOrNo> MultipleYesOrNo;

    private QuestionBaseModel<MultipleChoice> MultipleChoice;

    private QuestionBaseModel<Dropdown> Dropdown;

    private QuestionBaseModel<MultipleChoiceSingle> MultipleChoiceSingle;
    private QuestionBaseModel<FillBlankDropdown> FillBlankDropdownQuestion;

    private Dictionary<string, bool> Visibility = new Dictionary<string, bool>()
    {
        { QuestionTypeValues.DragAndDrop, false },
        { QuestionTypeValues.OrderingDragAndDrop, false },
        { QuestionTypeValues.Dropdown, false },
        { QuestionTypeValues.FillBlank, false },
        { QuestionTypeValues.FillBlankDropdown, false },
        { QuestionTypeValues.MultipleChoice, false },
        { QuestionTypeValues.MultipleChoiceSingle, false },
        { QuestionTypeValues.MultipleYesOrNo, false },
        { QuestionTypeValues.Ordering, false }
    };

    protected override void OnInitialized()
    {
        Visibility["DragAndDrop"] = true;
        FillBlankDropdownQuestion = SampleHelper.GetFillBlankDropdownQuestionSample();
        FillBlank = SampleHelper.GetFillBlankQuestionSample();
        MultipleChoiceSingle = SampleHelper.GetMultipleChoiceSingleQuestionSample();
        Dropdown = SampleHelper.GetDropdownQuestionSample();
        MultipleChoice = SampleHelper.GetMultipleChoiceQuestionSample();
        MultipleYesOrNo = SampleHelper.GetMultipleYesOrNoQuestionSample();
        OrderingQuestionModel = SampleHelper.GetOrderingQuestionSample();
        OrderingDnDQuestionModel = SampleHelper.GetOrderingDnDQuestionSample();
        DragAndDrop = SampleHelper.GetDragAndDropQuestionSample();
    }

    private void OpenOverlay(string type)
    {
        foreach (var item in Visibility.Keys)
        {
            Visibility[item] = false;
        }

        Visibility[type] = true;
    }
}