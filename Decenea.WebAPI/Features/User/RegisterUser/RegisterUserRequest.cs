namespace Decenea.WebAPI.Features.User.RegisterUser;

public class RegisterUserRequest
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string PhoneNumber { get; set; }
    public string CityId { get; set; }
    public string Password { get; set; }
}