
using Decenea.Common.Enums;

namespace Decenea.WebApp.Models.QuestionTypes;

public class MultipleChoice : QuestionBase
{
    public MultipleChoice() : base(QuestionType.MultipleChoice)
    {
        
    }
    public List<SubQuestion> SubQuestions { get; set; }  = new List<SubQuestion>();

    public class SubQuestion
    {
        public string? Text { get; set; }
        public List<Choice> Choices { get; set; } = new List<Choice>();
    }

    public class Choice
    {
        public string Text { get; set; }
        public bool Checked { get; set; }
    }
}