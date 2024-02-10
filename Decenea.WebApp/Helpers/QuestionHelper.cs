using Decenea.Common.Constants;
using Decenea.WebApp.Models;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Helpers;

public static class QuestionHelper
{
    public static QuestionBaseModel<FillBlank> GetFillBlankQuestionModel(this QuestionBaseModel questionBaseModel)
    {
        try
        {
            return questionBaseModel as QuestionBaseModel<FillBlank>;
        }
        catch (Exception e)
        {
            return new QuestionBaseModel<FillBlank>(new FillBlank());
        }
    }
}