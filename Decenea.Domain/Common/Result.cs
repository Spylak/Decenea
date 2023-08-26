namespace Decenea.Domain.Common;

public record Result<TValue, TException>
{
    public TValue? Value { get; init; }
    public TException? Exception { get; init; }
    public List<string>? Messages { get; init; }
    public bool IsExcepted { get; init; }
    
    public bool IsSuccess => !IsExcepted && Value is not null;
    
    private Result(TValue? value, List<string> messages)
    {
        IsExcepted = false;
        Value = value;
        Exception = default;
        Messages = messages;
    }
    private Result(TValue? value, string? message)
    {
        IsExcepted = false;
        Value = value;
        Exception = default;
        Messages = message is null ? null : new List<string>(){message};
    }

    private Result(TException? error, string? message)
    {
        IsExcepted = true;
        Exception = error;
        Value = default;
        Messages = message is null ? null : new List<string>(){message};
    }
    private Result(TException? error, List<string> messages)
    {
        IsExcepted = true;
        Exception = error;
        Value = default;
        Messages = messages;
    }

    public TResult Match<TResult>(
        Func<TValue, TResult> success,
        Func<TException, TResult> failure) =>
        !IsExcepted ? success(Value!) : failure(Exception!);
    
    public static Result<TValue, TException> Anticipated(TValue? value, string? message = null) => 
        new(value, message);
    
    public static Result<TValue, TException> Excepted(TException? error, string? message = null) => 
        new(error, message);
    
    public static Result<TValue, TException> Anticipated(TValue? value, List<string> messages) => 
        new(value, messages);
    
    public static Result<TValue, TException> Excepted(TException? error, List<string> messages) => 
        new(error, messages);

} 