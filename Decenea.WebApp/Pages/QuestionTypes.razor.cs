using Microsoft.AspNetCore.Components;
using Decenea.Common.Constants;
using Decenea.WebApp.Models.QuestionTypes;
using Decenea.WebApp.Services.IService;

namespace Decenea.WebApp.Pages;

public partial class QuestionTypes
{
    [Inject] private  ISampleService SampleService { get; set; }
    int activeIndex = 0;

    private DragAndDropQuestionModel DragAndDrop;
    
    private OrderingDnDQuestionModel OrderingDnDQuestionModel;
    
    private FillBlankQuestionModel FillBlank;

    private OrderingQuestionModel OrderingQuestionModel;

    private MultipleYesOrNoQuestionModel MultipleYesOrNo;

    private MultipleChoiceQuestionModel MultipleChoice;

    private DropdownQuestionModel Dropdown;

    private MultipleChoiceSingleQuestionModel MultipleChoiceSingle;
    private FillBlankDropdownQuestionModel FillBlankDropdownQuestion;

    private Dictionary<string, bool> Visibility = new Dictionary<string, bool>()
    {
        { QuestionTypeValues.DragAndDrop, false },
        { QuestionTypeValues.OrderingDragAndDrop, false },
        { QuestionTypeValues.Dropdown, false },
        { QuestionTypeValues.Fillblank, false },
        { QuestionTypeValues.FillblankDropdown, false },
        { QuestionTypeValues.MultipleChoice, false },
        { QuestionTypeValues.MultipleChoiceSingle, false },
        { QuestionTypeValues.MultipleYesOrNo, false },
        { QuestionTypeValues.Ordering, false }
    };

    protected override void OnInitialized()
    {
        Visibility["DragAndDrop"] = true;
        FillBlankDropdownQuestion = SampleService.GetFillBlankDropdownQuestionSample();
        FillBlank = SampleService.GetFillBlankQuestionSample();
        MultipleChoiceSingle = SampleService.GetMultipleChoiceSingleQuestionSample();
        Dropdown = SampleService.GetDropdownQuestionSample();
        MultipleChoice = SampleService.GetMultipleChoiceQuestionSample();
        MultipleYesOrNo = SampleService.GetMultipleYesOrNoQuestionSample();
        OrderingQuestionModel = SampleService.GetOrderingQuestionSample();
        OrderingDnDQuestionModel = SampleService.GetOrderingDnDQuestionSample();
        DragAndDrop = SampleService.GetDragAndDropQuestionSample();
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