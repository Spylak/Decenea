using ErrorOr;

namespace Decenea.Common.Extensions;

public static class ErrorOrExt
{
    public static Error ConcurrencyError( object data)
    {
        return Error.Conflict(description: "Concurrency Error",metadata: new Dictionary<string, object>()
        {
            { "data", data }
        });
    }
    
    public static Dictionary<string, string> ToErrorDictionary(this List<Error> errors)
    {
        return errors.ToDictionary(i => i.Code, i => i.Description);
    }
}