using Decenea.Common.Common;
using Decenea.Domain.Aggregates.TestAggregate;
using Decenea.Domain.Common;
using Decenea.Domain.Helpers;

namespace Decenea.Domain.Aggregates.UserAggregate;

public class User : AuditableAggregateRoot
{
    private readonly List<TestUser> _testUsers = new ();
    private IReadOnlyCollection<TestUser> TestUsers  => _testUsers.AsReadOnly();
    private readonly List<string> _userClaimIds = new ();
    private readonly List<string> _userTokenIds = new ();
    public int? CountryId { get; set; }
    public int RoleId { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string MiddleName { get; set; }
    
    public required string PasswordHash { get; set; }
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
    public DateTime? LastAccessFailed { get; set; }
    public bool IsVerified { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public DateTime? DateVerified { get; set; }
    public static Result<User,Exception> Create(string firstName,
        string email,
        string userName,
        string lastName,
        string middleName,
        string phoneNumber,
        string passHash)
    {
        var user = new User()
        {
            FirstName = firstName,
            Email = email,
            UserName = userName,
            LastName = lastName,
            MiddleName = middleName,
            PhoneNumber = phoneNumber,
            RoleId = Role.Guest,
            PasswordHash = passHash
        };
        
        return Result<User,Exception>.Anticipated(user);
    }
    
    public static Result<User,Exception> Update(User user, string firstName,
        string email,
        string userName,
        string lastName,
        string middleName,
        string phoneNumber)
    {
        user.FirstName = firstName;
        user.Email = email;
        user.UserName = userName;
        user.LastName = lastName;
        user.MiddleName = middleName;
        user.PhoneNumber = phoneNumber;
        
        return Result<User,Exception>.Anticipated(user);
    }
}   