



using Decenea.Common.Enums;

namespace Decenea.WebApp.Models.QuestionTypes;

public class Dropdown : QuestionBase
{
    public Dropdown() : base(QuestionType.Dropdown)
    {
        
    }
    public List<SubQuestion> SubQuestions { get; set; } = new List<SubQuestion>();
    public class SubQuestion
    {
        public string Text { get; set; }
        public List<Choice> Choices { get; set; }
    }
    public class Choice
    {
        public string Text { get; set; }
        public bool Checked { get; set; }
    }
}