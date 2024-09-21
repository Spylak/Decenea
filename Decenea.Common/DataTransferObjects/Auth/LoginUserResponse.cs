namespace Decenea.Common.DataTransferObjects.Auth;

public class LoginUserResponse
{
    public AuthTokensResponse AuthTokensResponse { get; set; } = new();
}