using Decenea.Domain.Common;
using Decenea.Domain.Helpers;

namespace Decenea.Domain.Aggregates.UserAggregate;

public class User : AggregateRoot
{

    private readonly List<string> _microAdIds = new();
    private readonly List<string> _userClaimIds = new();
    private readonly List<string> _userTokenIds = new();
    public required string CityId { get; set; }
    public int? CountryId { get; set; }
    public int RoleId { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string MiddleName { get; set; }
    
    public required string PasswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }
    public string FullName => $"{FirstName} {MiddleName} {LastName}";
    public string? UserName { get; set; }
    public string? Title { get; set; }
    public string? NormalizedUserName { get; set; }
    public string? NormalizedEmail { get; set; }
    public bool EmailConfirmed { get; set; }
    public string? PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public DateTime? LockoutEnd { get; set; }
    public bool LockoutEnabled { get; set; }
    public int AccessFailedCount { get; set; }
    public bool IsVerified { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public DateTime? DateVerified { get; set; }

    public static User Create(string firstName,
        string email,
        string userName,
        string lastName,
        string middleName,
        string phoneNumber,
        string cityId,
        string password)
    {
        var passwordHelper = new PassowordHelper();
        var passHash = passwordHelper.HashPasword(password, out var salt);
        
        var user = new User()
        {
            FirstName = firstName,
            Email = email,
            UserName = userName,
            LastName = lastName,
            MiddleName = middleName,
            PhoneNumber = phoneNumber,
            CityId = cityId,
            RoleId = Role.Guest,
            PasswordHash = passHash,
            PasswordSalt = salt
        };
        return user;
    }
}   