namespace Decenea.Domain.DataTransferObjects.ApplicationUser.LoginApplicationUser;

public class LoginApplicationUserDto
{
    public string ResidenceOf { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string Email { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public DateTime AccessTokenExpiryTime { get; set; }
}