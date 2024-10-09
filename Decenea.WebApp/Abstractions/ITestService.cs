using Decenea.Common.Common;
using Decenea.WebApp.Models;

namespace Decenea.WebApp.Abstractions;

public interface ITestService
{
    Task<ApiResponseResult> AddToTest<TQuestion>(TestModel testModel,TQuestion question) where TQuestion : class;
}