namespace Decenea.Domain.DataTransferObjects.Auth;

public class RegenerateAuthTokensRequestDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}