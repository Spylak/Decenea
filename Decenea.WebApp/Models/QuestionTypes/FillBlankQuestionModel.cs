using Decenea.Common.Constants;

namespace Decenea.WebApp.Models.QuestionTypes;

public class FillBlankQuestionModel : QuestionBaseModel
{
    public FillBlankQuestionModel()
    {
        QuestionType = QuestionTypeValues.Fillblank;
    }
    public List<SpaceOption> Options { get; set; }  = new List<SpaceOption>();

    public class SpaceOption
    {
        public int SpaceNo { get; set; }
        public string Text { get; set; }
    }
}