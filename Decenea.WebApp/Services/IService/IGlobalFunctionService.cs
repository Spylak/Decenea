namespace Decenea.WebApp.Services.IService;

public interface IGlobalFunctionService
{
    Task ConsoleLog<T>(T obj) where T : class;
    void Reload();
    HttpClient CreateClient(string subDomain="");
}