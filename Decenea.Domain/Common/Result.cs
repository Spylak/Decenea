namespace Decenea.Domain.Common;

public record Result<TValue, TError>
{
    public TValue? Value { get; init; }
    public TError? Error { get; init; }
    public bool IsError { get; init; }
    
    public bool IsSuccess => !IsError;
    
    public Result(TValue value)
    {
        IsError = false;
        Value = value;
        Error = default;
    }

    public Result(TError error)
    {
        IsError = true;
        Error = error;
        Value = default;
    }

    public static implicit operator Result<TValue, TError>(TValue value) => new(value);
    public static implicit operator Result<TValue, TError>(TError error) => new(error);

    public TResult Match<TResult>(
        Func<TValue, TResult> success,
        Func<TError, TResult> failure) =>
        !IsError ? success(Value!) : failure(Error!);
}