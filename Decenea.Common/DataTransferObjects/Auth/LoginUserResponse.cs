namespace Decenea.Common.DataTransferObjects.Auth;

public class LoginUserResponse
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string Email { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public DateTime AccessTokenExpiryTime { get; set; }
}