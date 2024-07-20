
using Decenea.Common.Enums;

namespace Decenea.WebApp.Models.QuestionTypes;

public class FillBlankDropdown : QuestionBase
{
    public FillBlankDropdown() : base(QuestionType.FillBlank)
    {
        
    }
    public List<SpaceOption> Options { get; set; } = new List<SpaceOption>();
    public class SpaceOption
    {
        public int SpaceNo { get; set; }
        public List<Choice> Choices { get; set; } = new List<Choice>();
    }
    public class Choice
    {
        public string Text { get; set; } = string.Empty;
        public bool Checked { get; set; }
    }
}