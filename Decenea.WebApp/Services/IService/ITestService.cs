using Decenea.WebApp.Models;

namespace Decenea.WebApp.Services.IService;

public interface ITestService
{
    Task<ResponseAPI> AddToTest<TQuestion>(Test test,TQuestion question) where TQuestion : class;
}