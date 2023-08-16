namespace Decenea.Domain.Common;

public record Result<TValue, TException>
{
    public TValue? Value { get; init; }
    public TException? Exception { get; init; }
    public string? Message { get; init; }
    public bool IsExcepted { get; init; }
    
    public bool IsSuccess => !IsExcepted && Value is not null;
    
    private Result(TValue? value, string? message)
    {
        IsExcepted = false;
        Value = value;
        Exception = default;
        Message = message;
    }

    private Result(TException? error, string? message)
    {
        IsExcepted = true;
        Exception = error;
        Value = default;
        Message = message;
    }

    public TResult Match<TResult>(
        Func<TValue, TResult> success,
        Func<TException, TResult> failure) =>
        !IsExcepted ? success(Value!) : failure(Exception!);
    
    public static Result<TValue, TException> Anticipated(TValue? value, string? message = null) => 
        new(value, message);
    
    public static Result<TValue, TException> Excepted(TException? error, string? message = null) => 
        new(error, message);

} 