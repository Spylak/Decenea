

using Decenea.Common.Constants;

namespace Decenea.WebApp.Models.QuestionTypes;

public class MultipleChoiceSingleQuestionModel : QuestionBaseModel
{
    public MultipleChoiceSingleQuestionModel()
    {
        QuestionType = QuestionTypeValues.MultipleChoiceSingle;
    }
    public List<SubQuestion> SubQuestions { get; set; } = new List<SubQuestion>();

    public class SubQuestion
    {
        public string Text { get; set; }
        public string Picked { get; set; }
        public List<string> Choices { get; set; } = new List<string>();
    }
}