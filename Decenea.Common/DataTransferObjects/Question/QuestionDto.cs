using Decenea.Common.Enums;

namespace Decenea.Common.DataTransferObjects.Question;

public class QuestionDto
{
    public string? Id { get; set; }
    public string Description { get; set; } = "";
    public string Title { get; set; } = "";
    public int? SecondsToAnswer { get; set; } 
    public int? Order { get; set; } 
    public double? Weight { get; set; }
    public required QuestionType QuestionType { get; init; }
    public string SerializedQuestionContent { get; set; } = string.Empty;
}