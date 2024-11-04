



using Decenea.Common.Enums;

namespace Decenea.Common.DataTransferObjects.Question.QuestionTypes;

public class MultipleChoiceSingle : QuestionBase
{
    public MultipleChoiceSingle() : base(QuestionType.MultipleChoiceSingle)
    {
        
    }
    public List<SubQuestion> SubQuestions { get; set; } = [];

    public class SubQuestion
    {
        public string Text { get; set; } = string.Empty;
        public string? Picked { get; set; } = string.Empty;
        public List<string> Choices { get; set; } = [];
    }
}