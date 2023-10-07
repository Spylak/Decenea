namespace Decenea.WebAppShared.Services.IServices;

public interface IGlobalFunctionService
{
    Task ConsoleLogAsync<T>(T obj) where T : class;
    Task ReloadAsync();
}