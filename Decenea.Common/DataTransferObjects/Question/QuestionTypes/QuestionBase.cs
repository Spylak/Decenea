using Decenea.Common.Enums;

namespace Decenea.Common.DataTransferObjects.Question.QuestionTypes;

public abstract class QuestionBase
{
    protected QuestionBase(QuestionType questionType)
    {
        QuestionType = questionType;
    }

    public QuestionType QuestionType { get; }
}