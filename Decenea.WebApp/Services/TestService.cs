using Decenea.WebApp.Database;
using Decenea.WebApp.Models;
using Decenea.WebApp.Models.QuestionTypes;
using Decenea.WebApp.Services.IService;

namespace Decenea.WebApp.Services;

public class TestService : ITestService
{
    private readonly IGlobalFunctionService _globalFunctionService;
    public TestService(IndexedDb indexedDb,
        IGlobalFunctionService globalFunctionService)
    {
        _globalFunctionService = globalFunctionService;
    }
    
    public async Task<ResponseAPI> AddToTest<TQuestion>(Test test,TQuestion question) where TQuestion : class
    {
        try
        {if (typeof(TQuestion) == typeof(DropdownQuestionModel))
            {
                test.DropdownQuestions.Add(question as DropdownQuestionModel);
            }
            else if (typeof(TQuestion) == typeof(DragAndDropQuestionModel))
            {
                test.DragAndDropQuestions.Add(question as DragAndDropQuestionModel);
            }
            else if (typeof(TQuestion) == typeof(FillBlankDropdownQuestionModel))
            {
                test.FillblankDropdownQuestions.Add(question as FillBlankDropdownQuestionModel);
            }
            else if (typeof(TQuestion) == typeof(FillBlankQuestionModel))
            {
                test.FillBlankQuestions.Add(question as FillBlankQuestionModel);
            }
            else if (typeof(TQuestion) == typeof(OrderingQuestionModel))
            {
                test.OrderingQuestions.Add(question as OrderingQuestionModel);
            }
            else if (typeof(TQuestion) == typeof(OrderingDnDQuestionModel))
            {
                test.OrderingDnDQuestions.Add(question as OrderingDnDQuestionModel);
            }
            else if (typeof(TQuestion) == typeof(MultipleChoiceQuestionModel))
            {
                test.MultipleChoiceQuestions.Add(question as MultipleChoiceQuestionModel);
            }
            else if (typeof(TQuestion) == typeof(MultipleYesOrNoQuestionModel))
            {
                test.MultipleYesOrNoQuestions.Add(question as MultipleYesOrNoQuestionModel);
            }
            else if (typeof(TQuestion) == typeof(MultipleChoiceSingleQuestionModel))
            {
                test.MultipleChoiceSingleQuestions.Add(question as MultipleChoiceSingleQuestionModel);
            }
            else
            {
                return new ResponseAPI(false);
            }
            return new ResponseAPI(true);
        }
        catch (Exception ex)
        {
            return new ResponseAPI(false,ex.Message);
        }
    }
}