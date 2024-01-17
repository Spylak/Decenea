
using Decenea.Common.Constants;

namespace Decenea.WebApp.Models.QuestionTypes;

public class FillBlankDropdownQuestionModel : QuestionBaseModel
{
    public FillBlankDropdownQuestionModel()
    {
        QuestionType = QuestionTypeValues.FillblankDropdown;
        Options = new List<SpaceOption>();
    }
    public List<SpaceOption> Options { get; set; } 
    public class SpaceOption
    {
        public int SpaceNo { get; set; }
        public List<Choice> Choices { get; set; } = new List<Choice>();
    }
    public class Choice
    {
        public string Text { get; set; }
        public bool Checked { get; set; }
    }
}