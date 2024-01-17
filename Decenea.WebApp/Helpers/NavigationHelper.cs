using Decenea.WebApp.Constants;

namespace Decenea.WebApp.Helpers;

public static class NavigationHelper
{
    public static string MyTests()
    {
        string uri = "mytests";
        return uri;
    }
    public static string Test(string? testId = null)
    {
        string uri = testId is null ? $"/{Routes.UpsertTest}" : $"/{Routes.UpsertTest}/{testId}";
        return uri;
    }
    
}