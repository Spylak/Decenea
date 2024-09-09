namespace Decenea.WebApp.Abstractions;

public interface IGlobalFunctionService
{
    Task ConsoleLogAsync<T>(T obj) where T : class;
    Task ReloadAsync();
    void Reload();
    HttpClient CreateClient(string subDomain = "");
}