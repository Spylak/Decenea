using Decenea.WebApp.Models;

namespace Decenea.WebApp.Extensions;

public static class TestExtensions
{
    public static ResponseAPI<dynamic?> RemoveQuestionById(this Test test, string questionId)
    {
        try
        {
            var question = test.GenericQuestionModels.FirstOrDefault(i => i.Id == questionId);
            if(question is null)
                return new ResponseAPI<dynamic?>(null, false, "No question found.");

            test.GenericQuestionModels.Remove(question);
            return new ResponseAPI<dynamic?>(question, true, "Successfully deleted question.");
        }
        catch (Exception ex)
        {
            return new ResponseAPI<dynamic?>(null, false, "No question found.");
        }
    }
}