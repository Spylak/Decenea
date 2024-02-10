

using Decenea.Common.Constants;

namespace Decenea.WebApp.Models.QuestionTypes;

public class MultipleChoiceSingle
{
    public List<SubQuestion> SubQuestions { get; set; } = new List<SubQuestion>();

    public class SubQuestion
    {
        public string Text { get; set; } = string.Empty;
        public string Picked { get; set; } = string.Empty;
        public List<string> Choices { get; set; } = new List<string>();
    }
}