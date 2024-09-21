using System.Text.Json;
using System.Text.Json.Serialization;
using Refit;

namespace Decenea.WebApp.Helpers;

public static class RefitHelper
{
    public static RefitSettings GetSettings()
    {
        return new RefitSettings()
        {
            ContentSerializer = (IHttpContentSerializer)new SystemTextJsonContentSerializer(new JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            })
        };
    }
}