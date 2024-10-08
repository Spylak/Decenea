namespace Decenea.Common.DataTransferObjects.Auth;

public class AuthTokensResponse
{
    public string AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public DateTime AccessTokenExpiryTime { get; set; }
};