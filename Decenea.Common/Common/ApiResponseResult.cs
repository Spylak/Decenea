namespace Decenea.Common.Common;

public record ApiResponseResult
{
    public ApiResponseResult()
    {
        IsError = true;
        Messages = [];
    }
    
    public ApiResponseResult(bool? isError = null)
    {
        IsError = isError ?? true;
        Messages = [];
        Data = null;
    }
    
    public ApiResponseResult(bool? isError = null, Dictionary<string, string>? messages = null)
    {
        IsError = isError ?? true;
        Messages = messages ?? [];
        Data = null;
    }
    
    public ApiResponseResult(bool? isError = null, string? message = null)
    {
        if (message is not null)
            Messages["Generic"] = message; 
        IsError = isError ?? true;
        Data = null;
    }
    
    public ApiResponseResult(bool? isError = null, object? data = null, Dictionary<string, string>? messages = null)
    {
        IsError = isError ?? true;
        Messages = messages ?? [];
        Data = data;
    }
    
    public ApiResponseResult(bool? isError = null, object? data = null, string? message = null)
    {
        IsError = isError ?? true;
        if (message is not null)
            Messages["Generic"] = message; 
        Data = data;
    }

    public bool IsError { get; set; }
    public Dictionary<string, string> Messages { get; set; } = new();
    public object? Data { get; set; }
}

public record ApiResponseResult<T> : ApiResponseResult where T : class?
{
    public ApiResponseResult()
    {
        IsError = true;
        Messages = [];
    }

    public ApiResponseResult(T? data = null, bool? isError = null, Dictionary<string, string>? messages = null) : base(isError, messages)
    {
        Data = data;
    }
    
    public ApiResponseResult(T? data = null, bool? isError = null, string? message = null) : base(isError, message)
    {
        Data = data;
    }
    public new T? Data { get; set; }
}