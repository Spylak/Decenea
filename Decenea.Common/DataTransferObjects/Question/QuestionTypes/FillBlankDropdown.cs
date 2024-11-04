
using Decenea.Common.Enums;

namespace Decenea.Common.DataTransferObjects.Question.QuestionTypes;

public class FillBlankDropdown : QuestionBase
{
    public FillBlankDropdown() : base(QuestionType.FillBlankDropdown)
    {
        
    }
    public List<SpaceOption> Options { get; set; } = [];
    public class SpaceOption
    {
        public int SpaceNo { get; set; }
        public List<Choice> Choices { get; set; } = [];
    }
    public class Choice
    {
        public string Text { get; set; } = string.Empty;
        public bool Checked { get; set; }
    }
}