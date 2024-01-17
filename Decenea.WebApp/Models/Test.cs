using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Models;

public class Test
{
    public string? Id { get; set; } = Guid.NewGuid().ToString();
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int MinutesToComplete { get; set; }
    public DateTime StartingTime { get; set; }
    public DateTime FinishTime => StartingTime.AddMinutes(MinutesToComplete);
    public bool IsActive { get; set; }
    public bool IsDraft { get; set; }
    public List<DragAndDropQuestionModel> DragAndDropQuestions { get; set; } = new List<DragAndDropQuestionModel>();
    public List<DropdownQuestionModel> DropdownQuestions { get; set; } = new List<DropdownQuestionModel>();
    public List<FillBlankQuestionModel> FillBlankQuestions { get; set; } = new List<FillBlankQuestionModel>();
    public List<FillBlankDropdownQuestionModel> FillblankDropdownQuestions { get; set; } = new List<FillBlankDropdownQuestionModel>();
    public List<MultipleChoiceQuestionModel> MultipleChoiceQuestions { get; set; } = new List<MultipleChoiceQuestionModel>();

    public List<MultipleChoiceSingleQuestionModel> MultipleChoiceSingleQuestions { get; set; } =
        new List<MultipleChoiceSingleQuestionModel>();

    public List<MultipleYesOrNoQuestionModel> MultipleYesOrNoQuestions { get; set; } = new List<MultipleYesOrNoQuestionModel>();
    public List<OrderingQuestionModel> OrderingQuestions { get; set; } = new List<OrderingQuestionModel>();
    public List<OrderingDnDQuestionModel> OrderingDnDQuestions { get; set; } = new List<OrderingDnDQuestionModel>();
}