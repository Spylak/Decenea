using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Services.IService;

public interface ISampleService
{
    DragAndDropQuestionModel GetDragAndDropQuestionSample();
    DropdownQuestionModel GetDropdownQuestionSample();
    OrderingQuestionModel GetOrderingQuestionSample();
    FillBlankDropdownQuestionModel GetFillBlankDropdownQuestionSample();
    FillBlankQuestionModel GetFillBlankQuestionSample();
    MultipleChoiceSingleQuestionModel GetMultipleChoiceSingleQuestionSample();
    MultipleYesOrNoQuestionModel GetMultipleYesOrNoQuestionSample();
    MultipleChoiceQuestionModel GetMultipleChoiceQuestionSample();
    OrderingDnDQuestionModel GetOrderingDnDQuestionSample();
}