namespace Decenea.WebApp.Abstractions;

public interface IGlobalFunctionService
{
    Task ConsoleLogAsync(object obj);
    Task ReloadAsync();
    void Reload();
    HttpClient CreateClient(string subDomain = "");
}