using Decenea.Common.Common;
using Decenea.WebApp.Models;

namespace Decenea.WebApp.Extensions;

public static class TestExtensions
{
    public static ApiResponseResult<dynamic?> RemoveQuestionById(this TestModel testModel, string questionId)
    {
        try
        {
            var question = testModel.GenericQuestionModels.FirstOrDefault(i => i.Id == questionId);
            if(question is null)
                return new ApiResponseResult<dynamic?>(null, true, "No question found.");

            testModel.GenericQuestionModels.Remove(question);
            return new ApiResponseResult<dynamic?>(question, false, "Successfully deleted question.");
        }
        catch (Exception ex)
        {
            return new ApiResponseResult<dynamic?>(null, true, "No question found.");
        }
    }
}