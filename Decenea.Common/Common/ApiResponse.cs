namespace Decenea.Common.Common;

public class ApiResponse
{
    public ApiResponse(bool? isSuccess = null, List<string>? messages = null)
    {
        IsSuccess = isSuccess ?? false;
        Messages = messages ?? new List<string>();
    }
    public ApiResponse(bool? isSuccess = null, string? message = null)
    {
        IsSuccess = isSuccess ?? false;
        Messages = new List<string>();
        if(message is not null)
            Messages.Add(message);
    }

    public bool? IsSuccess { get; set; }
    public List<string> Messages { get; set; } 
}

public class ApiResponse<T> : ApiResponse where T : class?
{
    public ApiResponse(T? data = null, bool? isSuccess = null, List<string>? messages = null) : base(isSuccess, messages)
    {
        Data = data;
    }
    
    public ApiResponse(T? data = null, bool? isSuccess = null, string? message = null) : base(isSuccess, message)
    {
        Data = data;
    }
    public T? Data { get; set; }
}