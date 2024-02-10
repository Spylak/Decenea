

using Decenea.Common.Constants;

namespace Decenea.WebApp.Models.QuestionTypes;

public class Dropdown
{
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