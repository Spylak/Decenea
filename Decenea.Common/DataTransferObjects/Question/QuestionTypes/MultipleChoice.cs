
using Decenea.Common.Enums;

namespace Decenea.Common.DataTransferObjects.Question.QuestionTypes;

public class MultipleChoice : QuestionBase
{
    public MultipleChoice() : base(QuestionType.MultipleChoice)
    {
        
    }
    public List<SubQuestion> SubQuestions { get; set; }  = [];

    public class SubQuestion
    {
        public string? Text { get; set; }
        public List<Choice> Choices { get; set; } = [];
    }

    public class Choice
    {
        public string Text { get; set; }
        public bool Checked { get; set; }
    }
}