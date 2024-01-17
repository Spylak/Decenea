
using Decenea.WebApp.Helpers;
using Decenea.WebApp.Models;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Extensions;

public static class TestExtension
{
    public static List<QuestionBaseModel> GetBaseQuestions(this Test test)
    {
        var questionBaseList = new List<QuestionBaseModel>();
        
        foreach (var question in test.DropdownQuestions)
        {
            questionBaseList.Add(question);
        }
        foreach (var question in test.OrderingQuestions)
        {
            questionBaseList.Add(question);
        }
        foreach (var question in test.FillBlankQuestions)
        {
            questionBaseList.Add(question);
        }
        foreach (var question in test.MultipleChoiceQuestions)
        {
            questionBaseList.Add(question);
        }
        foreach (var question in test.DragAndDropQuestions)
        {
            questionBaseList.Add(question);
        }
        foreach (var question in test.FillblankDropdownQuestions)
        {
            questionBaseList.Add(question);
        }
        foreach (var question in test.MultipleChoiceSingleQuestions)
        {
            questionBaseList.Add(question);
        }
        foreach (var question in test.OrderingDnDQuestions)
        {
            questionBaseList.Add(question);
        }
        foreach (var question in test.MultipleYesOrNoQuestions)
        {
            questionBaseList.Add(question);
        }
        return questionBaseList;
    }
    public static ResponseAPI<dynamic?> RemoveQuestionById(this Test test, string questionType, string questionId)
    {
        return QuestionHelper.RemoveQuestionById(test, questionType, questionId);
    }
}