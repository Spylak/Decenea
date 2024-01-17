using Decenea.Common.Constants;

namespace Decenea.WebApp.Models.QuestionTypes;

public class MultipleYesOrNoQuestionModel : QuestionBaseModel
{
    public MultipleYesOrNoQuestionModel()
    {
        QuestionType = QuestionTypeValues.MultipleYesOrNo;
    }
    public List<SubQuestion> SubQuestions { get; set; } = new List<SubQuestion>();
    public class SubQuestion
    {
        public string Text { get; set; }
        public bool Checked { get; set; }
    }
}