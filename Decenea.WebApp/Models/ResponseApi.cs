namespace Decenea.WebApp.Models;

public class ResponseAPI
{
    public ResponseAPI(bool isSuccess, string message = "")
    {
        IsSuccess = isSuccess;
        Message = message;
    }
    public bool IsSuccess { get; init; }
    public string Message { get; init; }
}
public class ResponseAPI<T> : ResponseAPI where T : class?
{
    public ResponseAPI(T? data, bool isSuccess, string message = "") : base(isSuccess, message)
    {
        Data = data;
    }
    public T? Data { get; init; }
}