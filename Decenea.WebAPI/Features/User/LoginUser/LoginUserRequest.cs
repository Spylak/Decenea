namespace Decenea.Common.DataTransferObjects.ApplicationUser;

public class LoginUserRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool? RememberMe { get; set; }
}