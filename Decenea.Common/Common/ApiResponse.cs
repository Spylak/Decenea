namespace Decenea.Common.Common;

public record ApiResponse
{
    public ApiResponse()
    {
        IsSuccess = false;
        Messages = [];
    }
    
    public ApiResponse(bool? isSuccess = null)
    {
        IsSuccess = isSuccess ?? false;
        Messages = [];
        Data = null;
    }
    
    public ApiResponse(bool? isSuccess = null, List<string>? messages = null)
    {
        IsSuccess = isSuccess ?? false;
        Messages = messages ?? [];
        Data = null;
    }
    
    public ApiResponse(bool? isSuccess = null, string? message = null)
    {
        IsSuccess = isSuccess ?? false;
        Messages = [ message ?? "" ];
        Data = null;
    }
    
    public ApiResponse(bool? isSuccess = null, object? data = null, List<string>? messages = null)
    {
        IsSuccess = isSuccess ?? false;
        Messages = messages ?? [];
        Data = data;
    }
    
    public ApiResponse(bool? isSuccess = null, object? data = null, string? message = null)
    {
        IsSuccess = isSuccess ?? false;
        Messages = [ message ?? "" ];
        Data = data;
    }

    public bool? IsSuccess { get; set; }
    public List<string> Messages { get; set; } 
    public object? Data { get; set; }
}

public record ApiResponse<T> : ApiResponse where T : class?
{
    public ApiResponse()
    {
        IsSuccess = false;
        Messages = [];
    }

    public ApiResponse(T? data = null, bool? isSuccess = null, List<string>? messages = null) : base(isSuccess, messages)
    {
        Data = data;
    }
    
    public ApiResponse(T? data = null, bool? isSuccess = null, string? message = null) : base(isSuccess, message)
    {
        Data = data;
    }
    public new T? Data { get; set; }
}