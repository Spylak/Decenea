
using Decenea.Common.Enums;
using Decenea.WebApp.Models;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Helpers;

public static class QuestionHelper
{
    public static GenericQuestionModel<FillBlank> GetFillBlankQuestionModel(this GenericQuestionModel genericQuestionModel)
    {
        try
        {
            return genericQuestionModel as GenericQuestionModel<FillBlank>;
        }
        catch (Exception e)
        {
            return new GenericQuestionModel<FillBlank>(new FillBlank())
            {
                QuestionType = QuestionType.FillBlank
            };
        }
    }
}