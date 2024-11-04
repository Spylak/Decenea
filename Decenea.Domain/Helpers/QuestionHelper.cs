using System.Text.Json;
using Decenea.Common.DataTransferObjects.Question.QuestionTypes;
using Decenea.Common.Enums;

namespace Decenea.Domain.Helpers;

public static class QuestionHelper
{
    public static string SetUnAnsweredContent(QuestionType questionType, string serializedQuestionContent)
    {
        switch (questionType)
        {
            case QuestionType.FillBlank:
                var fillBlank = JsonSerializer.Deserialize<FillBlank>(serializedQuestionContent)!;
                foreach (var spaceOption in fillBlank.Options)
                {
                    spaceOption.Text = string.Empty;
                }
                return JsonSerializer.Serialize(fillBlank);
            case QuestionType.MultipleChoice:
                var multipleChoice = JsonSerializer.Deserialize<MultipleChoice>(serializedQuestionContent)!;
                foreach (var subQuestion in multipleChoice.SubQuestions)
                {
                    foreach (var choice in subQuestion.Choices)
                    {
                        choice.Checked = false;
                    }
                }
                return JsonSerializer.Serialize(multipleChoice);
            case QuestionType.FillBlankDropdown:
                var fillBlankDropdown = JsonSerializer.Deserialize<FillBlankDropdown>(serializedQuestionContent)!;
                foreach (var spaceOption in fillBlankDropdown.Options)
                {
                    foreach (var choice in spaceOption.Choices)
                    {
                        choice.Checked = false;
                    }
                }
                return JsonSerializer.Serialize(fillBlankDropdown);
            case QuestionType.MultipleChoiceSingle:
                var multipleChoiceSingle = JsonSerializer.Deserialize<MultipleChoiceSingle>(serializedQuestionContent)!;
                foreach (var subQuestion in multipleChoiceSingle.SubQuestions)
                {
                    subQuestion.Picked = string.Empty;
                }
                return JsonSerializer.Serialize(multipleChoiceSingle);
            case QuestionType.MultipleYesOrNo:
                var multipleYesOrNo = JsonSerializer.Deserialize<MultipleYesOrNo>(serializedQuestionContent)!;
                foreach (var subQuestion in multipleYesOrNo.SubQuestions)
                {
                    subQuestion.Checked = false;
                }
                return JsonSerializer.Serialize(multipleYesOrNo);
            case QuestionType.Dropdown:
                var dropdown = JsonSerializer.Deserialize<Dropdown>(serializedQuestionContent)!;
                foreach (var subQuestion in dropdown.SubQuestions)
                {
                    foreach (var choice in subQuestion.Choices)
                    {
                        choice.Checked = false;
                    }
                }
                return JsonSerializer.Serialize(dropdown);
            case QuestionType.Ordering:
                var ordering = JsonSerializer.Deserialize<Ordering>(serializedQuestionContent)!;
                foreach (var choice in ordering.Choices)
                {
                    choice.Active = false;
                }
                return JsonSerializer.Serialize(ordering);
            case QuestionType.DragAndDrop:
                var dragAndDrop = JsonSerializer.Deserialize<DragAndDrop>(serializedQuestionContent)!;
                foreach (var choice in dragAndDrop.Choices)
                {
                    choice.Selector = "0";
                }
                return JsonSerializer.Serialize(dragAndDrop);
            case QuestionType.OrderingDragAndDrop:
                var orderingDragAndDrop = JsonSerializer.Deserialize<OrderingDragAndDrop>(serializedQuestionContent)!;
                foreach (var choice in orderingDragAndDrop.Choices)
                {
                    choice.Selector = "0";
                }
                return JsonSerializer.Serialize(orderingDragAndDrop);
            default:
                return serializedQuestionContent;
        }
    }
}