namespace Decenea.Common.Common;

public record Result<TSuccess, TError>
{
    public TSuccess? SuccessValue { get; init; }
    public TError? ErrorValue { get; init; }
    public List<string>? Messages { get; init; }
    public bool IsError { get; init; }
    
    public bool IsSuccess { get; init; }
    
    private Result(TSuccess? successValue, List<string>? messages, bool? isSuccess)
    {
        IsError = false;
        IsSuccess = isSuccess ?? successValue is not null;
        SuccessValue = successValue;
        ErrorValue = default;
        Messages = messages ?? new List<string>();
    }
    private Result(TError? error, List<string>? messages, bool? isSuccess)
    {
        IsError = true;
        IsSuccess = isSuccess ?? false;
        ErrorValue = error;
        SuccessValue = default;
        Messages = messages ?? new List<string>();
    }

    public TResult Match<TResult>(
        Func<TSuccess, TResult> success,
        Func<TError, TResult> failure) =>
        !IsError ? success(SuccessValue!) : failure(ErrorValue!);
    
    public static Result<TSuccess, TError> Anticipated(TSuccess? value, List<string>? messages = null, bool? isSuccess = null) => 
        new(value, messages, isSuccess);
    
    public static Result<TSuccess, TError> Excepted(TError? error, List<string>? messages = null, bool? isSuccess = null) => 
        new(error, messages, isSuccess);
} 