namespace Decenea.Common.Common;

public record Result<TValue, TError>
{
    public TValue? Value { get; init; }
    public TError? Exception { get; init; }
    public List<string>? Messages { get; init; }
    public bool IsExcepted { get; init; }
    
    public bool IsSuccess { get; init; }
    
    private Result(TValue? value, List<string>? messages, bool? isSuccess)
    {
        IsExcepted = false;
        IsSuccess = isSuccess ?? value is not null;
        Value = value;
        Exception = default;
        Messages = messages ?? new List<string>();
    }
    private Result(TValue? value, string? message, bool? isSuccess)
    {
        IsExcepted = false;
        IsSuccess = isSuccess ?? value is not null;
        Value = value;
        Exception = default;
        Messages = message is null ? new List<string>() : new List<string>(){message};
    }

    private Result(TError? error, string? message, bool? isSuccess)
    {
        IsExcepted = true;
        IsSuccess = isSuccess ?? false;
        Exception = error;
        Value = default;
        Messages = message is null ? new List<string>() : new List<string>(){message};
    }
    private Result(TError? error, List<string>? messages, bool? isSuccess)
    {
        IsExcepted = true;
        IsSuccess = isSuccess ?? false;
        Exception = error;
        Value = default;
        Messages = messages ?? new List<string>();
    }

    public TResult Match<TResult>(
        Func<TValue, TResult> success,
        Func<TError, TResult> failure) =>
        !IsExcepted ? success(Value!) : failure(Exception!);
    
    public static Result<TValue, TError> Anticipated(TValue? value, string message, bool? isSuccess = null) => 
        new(value, message, isSuccess);
    
    public static Result<TValue, TError> Excepted(TError? error, string message, bool? isSuccess = null) => 
        new(error, message, isSuccess);
    
    public static Result<TValue, TError> Anticipated(TValue? value, List<string>? messages = null, bool? isSuccess = null) => 
        new(value, messages, isSuccess);
    
    public static Result<TValue, TError> Excepted(TError? error, List<string>? messages = null, bool? isSuccess = null) => 
        new(error, messages, isSuccess);
} 