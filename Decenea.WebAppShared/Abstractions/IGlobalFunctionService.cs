namespace Decenea.WebAppShared.Abstractions;

public interface IGlobalFunctionService
{
    Task ConsoleLogAsync<T>(T obj) where T : class;
    Task ReloadAsync();
}