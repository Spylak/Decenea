using Decenea.Common.Common;
using Decenea.WebApp.Abstractions;
using Decenea.WebApp.Database;
using Decenea.WebApp.Models;

namespace Decenea.WebApp.Services;

public class TestService : ITestService
{
    private readonly IGlobalFunctionService _globalFunctionService;
    public TestService(IndexedDb indexedDb,
        IGlobalFunctionService globalFunctionService)
    {
        _globalFunctionService = globalFunctionService;
    }
    
    public async Task<ApiResponseResult> AddToTest<TQuestion>(TestModel testModel,TQuestion question) where TQuestion : class
    {
        try
        {
            return new ApiResponseResult(false);
        }
        catch (Exception ex)
        {
            return new ApiResponseResult(true,ex.Message);
        }
    }
}