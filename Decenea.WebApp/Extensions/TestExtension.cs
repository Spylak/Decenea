
using Decenea.WebApp.Helpers;
using Decenea.WebApp.Models;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Extensions;

public static class TestExtension
{
    public static ResponseAPI<dynamic?> RemoveQuestionById(this Test test, string questionId)
    {
        try
        {
            var question = test.QuestionBaseModels.FirstOrDefault(i => i.Id == questionId);
            if(question is null)
                return new ResponseAPI<dynamic?>(null, false, "No question found.");

            test.QuestionBaseModels.Remove(question);
            return new ResponseAPI<dynamic?>(question, true, "Successfully deleted question.");
        }
        catch (Exception ex)
        {
            return new ResponseAPI<dynamic?>(null, false, "No question found.");
        }
    }
}