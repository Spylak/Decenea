using Decenea.Common.Enums;

namespace Decenea.WebApp.Models.QuestionTypes;

public abstract class QuestionBase
{
    protected QuestionBase(QuestionType questionType)
    {
        QuestionType = questionType;
    }

    public QuestionType QuestionType { get; }
}