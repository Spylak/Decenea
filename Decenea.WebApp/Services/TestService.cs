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
        {
            return new ResponseAPI(true);
        }
        catch (Exception ex)
        {
            return new ResponseAPI(false,ex.Message);
        }
    }
}