using Decenea.Common.Enums;

namespace Decenea.WebApp.Models.QuestionTypes;

public class FillBlank : QuestionBase
{
    public FillBlank() : base(QuestionType.FillBlank)
    {
        
    }
    public List<SpaceOption> Options { get; set; }  = new List<SpaceOption>();

    public class SpaceOption
    {
        public SpaceOption()
        {
            
        }
        public int SpaceNo { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}