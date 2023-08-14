namespace Decenea.Domain.DataTransferObjects.ApplicationUser.RegisterApplicationUser;

public class RegisterApplicationUserRequest
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string PhoneNumber { get; set; }
    public string ResidenceOf { get; set; }
    public string Password { get; set; }
}