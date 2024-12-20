

using Decenea.Common.Enums;

namespace Decenea.Common.DataTransferObjects.Question.QuestionTypes;

public class MultipleYesOrNo : QuestionBase
{
    public MultipleYesOrNo() : base(QuestionType.MultipleYesOrNo)
    {
        
    }
    public List<SubQuestion> SubQuestions { get; set; } = new List<SubQuestion>();
    public class SubQuestion
    {
        public string Text { get; set; }
        public bool Checked { get; set; }
    }
}