using Decenea.Common.Constants;
using Decenea.WebApp.Models;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Helpers;

public static class QuestionHelper
{
    public static object? GetQuestion(Test test, string questionType, string questionId)
    {
        switch (questionType)
        {
            case QuestionTypeValues.Dropdown:
                var dropdownQuestion = test.DropdownQuestions.FirstOrDefault(i => i.Id == questionId);
                return dropdownQuestion;
            case QuestionTypeValues.Ordering:
                var orderingQuestion = test.OrderingQuestions.FirstOrDefault(i => i.Id == questionId);
                return orderingQuestion;
            case QuestionTypeValues.Fillblank:
                var fillBlankQuestion = test.FillBlankQuestions.FirstOrDefault(i => i.Id == questionId);
                return fillBlankQuestion;
            case QuestionTypeValues.MultipleChoice:
                var multipleChoiceQuestion = test.MultipleChoiceQuestions.FirstOrDefault(i => i.Id == questionId);
                return multipleChoiceQuestion;
            case QuestionTypeValues.DragAndDrop:
                var dragAndDropQuestion = test.DragAndDropQuestions.FirstOrDefault(i => i.Id == questionId);
                return dragAndDropQuestion;
            case QuestionTypeValues.FillblankDropdown:
                var inTextDropdownQuestion = test.FillblankDropdownQuestions.FirstOrDefault(i => i.Id == questionId);
                return inTextDropdownQuestion;
            case QuestionTypeValues.MultipleChoiceSingle:
                var multipleChoiceSingleQuestion = test.MultipleChoiceSingleQuestions.FirstOrDefault(i => i.Id == questionId);
                return multipleChoiceSingleQuestion;
            case QuestionTypeValues.OrderingDragAndDrop:
                var orderingDnDQuestion = test.OrderingDnDQuestions.FirstOrDefault(i => i.Id == questionId);
                return orderingDnDQuestion;
            case QuestionTypeValues.MultipleYesOrNo:
                var multipleYesOrNoQuestion = test.MultipleYesOrNoQuestions.FirstOrDefault(i => i.Id == questionId);
                return multipleYesOrNoQuestion;
            default:
                return null;
        }
    }

    public static ResponseAPI<dynamic?> RemoveQuestionById(Test test, string questionType, string questionId)
    {
        switch (questionType)
        {
            case nameof(DropdownQuestionModel):
                var dropdownQuestion = test.DropdownQuestions.FirstOrDefault(i => i.Id == questionId);
                if (dropdownQuestion is null)
                    return new ResponseAPI<dynamic?>(null, false, "No question found.");
                test.DropdownQuestions.Remove(dropdownQuestion);
                return new ResponseAPI<dynamic?>(dropdownQuestion, true, "Successfully deleted question.");
            case nameof(OrderingQuestionModel):
                var orderingQuestion = test.OrderingQuestions.FirstOrDefault(i => i.Id == questionId);
                if (orderingQuestion is null)
                    return new ResponseAPI<dynamic?>(null, false, "No question found.");
                test.OrderingQuestions.Remove(orderingQuestion);
                return new ResponseAPI<dynamic?>(orderingQuestion, true, "Successfully deleted question.");
            case nameof(FillBlankQuestionModel):
                var fillBlankQuestion = test.FillBlankQuestions.FirstOrDefault(i => i.Id == questionId);
                if (fillBlankQuestion is null)
                    return new ResponseAPI<dynamic?>(null, false, "No question found.");
                test.FillBlankQuestions.Remove(fillBlankQuestion);
                return new ResponseAPI<dynamic?>(fillBlankQuestion, true, "Successfully deleted question.");
            case nameof(MultipleChoiceQuestionModel):
                var multipleChoiceQuestion = test.MultipleChoiceQuestions.FirstOrDefault(i => i.Id == questionId);
                if (multipleChoiceQuestion is null)
                    return new ResponseAPI<dynamic?>(null, false, "No question found.");
                test.MultipleChoiceQuestions.Remove(multipleChoiceQuestion);
                return new ResponseAPI<dynamic?>(multipleChoiceQuestion, true, "Successfully deleted question.");
            case nameof(DragAndDropQuestionModel):
                var dragAndDropQuestion = test.DragAndDropQuestions.FirstOrDefault(i => i.Id == questionId);
                if (dragAndDropQuestion is null)
                    return new ResponseAPI<dynamic?>(null, false, "No question found.");
                test.DragAndDropQuestions.Remove(dragAndDropQuestion);
                return new ResponseAPI<dynamic?>(dragAndDropQuestion, true, "Successfully deleted question.");
            case nameof(FillBlankDropdownQuestionModel):
                var inTextDropdownQuestion = test.FillblankDropdownQuestions.FirstOrDefault(i => i.Id == questionId);
                if (inTextDropdownQuestion is null)
                    return new ResponseAPI<dynamic?>(null, false, "No question found.");
                test.FillblankDropdownQuestions.Remove(inTextDropdownQuestion);
                return new ResponseAPI<dynamic?>(inTextDropdownQuestion, true, "Successfully deleted question.");
            case nameof(MultipleChoiceSingleQuestionModel):
                var multipleChoiceSingleQuestion = test.MultipleChoiceSingleQuestions.FirstOrDefault(i => i.Id == questionId);
                if (multipleChoiceSingleQuestion is null)
                    return new ResponseAPI<dynamic?>(null, false, "No question found.");
                test.MultipleChoiceSingleQuestions.Remove(multipleChoiceSingleQuestion);
                return new ResponseAPI<dynamic?>(multipleChoiceSingleQuestion, true,
                    "Successfully deleted question.");
            case nameof(OrderingDnDQuestionModel):
                var orderingDnDQuestion = test.OrderingDnDQuestions.FirstOrDefault(i => i.Id == questionId);
                if (orderingDnDQuestion is null)
                    return new ResponseAPI<dynamic?>(null, false, "No question found.");
                test.OrderingDnDQuestions.Remove(orderingDnDQuestion);
                return new ResponseAPI<dynamic?>(orderingDnDQuestion, true, "Successfully deleted question.");
            case nameof(MultipleYesOrNoQuestionModel):
                var multipleYesOrNoQuestion = test.MultipleYesOrNoQuestions.FirstOrDefault(i => i.Id == questionId);
                if (multipleYesOrNoQuestion is null)
                    return new ResponseAPI<dynamic?>(null, false, "No question found.");
                test.MultipleYesOrNoQuestions.Remove(multipleYesOrNoQuestion);
                return new ResponseAPI<dynamic?>(multipleYesOrNoQuestion, true, "Successfully deleted question.");
            default:
                return new ResponseAPI<dynamic?>(null, false, "No question found.");
        }
    }
}