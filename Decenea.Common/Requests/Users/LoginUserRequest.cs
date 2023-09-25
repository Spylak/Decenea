namespace Decenea.Common.Requests.Users;

public class LoginUserRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool? RememberMe { get; set; }
}