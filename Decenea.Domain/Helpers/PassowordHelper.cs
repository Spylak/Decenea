using System.Security.Cryptography;
using System.Text;
using Decenea.Domain.Common;
using Decenea.Common.Common;
using Decenea.Domain.Aggregates.UserAggregate;
using Microsoft.AspNetCore.Identity;

namespace Decenea.Domain.Helpers;

public class PassowordHelper
{
    private readonly PasswordOptions _passwordOptions;
    private const int _keySize = 64;
    private const int _iterations = 350000;
    private HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;
    
    public string HashPasword(string password, out byte[] salt)
    {
        salt = RandomNumberGenerator.GetBytes(_keySize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            _iterations,
            _hashAlgorithm,
            _keySize);
        return Convert.ToHexString(hash);
    }
    
    public bool VerifyPassword(string password, string hash, byte[] salt)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, _iterations, _hashAlgorithm, _keySize);
        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
    }
    
    public PassowordHelper(PasswordOptions? passwordOptions = null)
    {
        _passwordOptions = passwordOptions ?? new PasswordOptions()
        {
            RequiredLength = 8,
            RequiredUniqueChars = 4,
            RequireDigit = true,
            RequireLowercase = true,
            RequireNonAlphanumeric = true,
            RequireUppercase = true
        };
    }
    public Result<object,Exception> ValidatePassword(string password)
    {
            if (string.IsNullOrWhiteSpace(password))
                return Result<object,Exception>.Anticipated(null,"Password cannot be empty or whitespace.");

            if (password.Length < _passwordOptions.RequiredLength)
                return Result<object,Exception>.Anticipated(null,$"Password must be at least {_passwordOptions.RequiredLength} characters long.");

            if (password.Distinct().Count() < _passwordOptions.RequiredUniqueChars)
                return Result<object,Exception>.Anticipated(null,$"Password must have at least {_passwordOptions.RequiredUniqueChars} unique characters.");

            if (_passwordOptions.RequireDigit && !password.Any(char.IsDigit))
                return Result<object,Exception>.Anticipated(null,"Password must contain at least one digit.");

            if (_passwordOptions.RequireLowercase && !password.Any(char.IsLower))
                return Result<object,Exception>.Anticipated(null,"Password must contain at least one lowercase letter.");

            if (_passwordOptions.RequireUppercase && !password.Any(char.IsUpper))
                return Result<object,Exception>.Anticipated(null,"Password must contain at least one uppercase letter.");

            if (_passwordOptions.RequireNonAlphanumeric && password.All(char.IsLetterOrDigit))
                return Result<object,Exception>.Anticipated(null,"Password must contain at least one non-alphanumeric character.");

            return Result<object,Exception>.Anticipated(true,"Password is valid.");
    }

    public string GenerateRandomPassword()
    {

        string[] randomChars = new[]
        {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",
            "abcdefghijkmnopqrstuvwxyz",
            "0123456789",
            "!@#$%^&*()_+-="
        };

        Random rand = new(Environment.TickCount);
        List<char> chars = new List<char>();

        if (_passwordOptions.RequireUppercase)
            chars.Insert(rand.Next(0, chars.Count),
                randomChars[0][rand.Next(0, randomChars[0].Length)]);

        if (_passwordOptions.RequireLowercase)
            chars.Insert(rand.Next(0, chars.Count),
                randomChars[1][rand.Next(0, randomChars[1].Length)]);

        if (_passwordOptions.RequireDigit)
            chars.Insert(rand.Next(0, chars.Count),
                randomChars[2][rand.Next(0, randomChars[2].Length)]);

        if (_passwordOptions.RequireNonAlphanumeric)
            chars.Insert(rand.Next(0, chars.Count),
                randomChars[3][rand.Next(0, randomChars[3].Length)]);

        for (int i = chars.Count;
             i < _passwordOptions.RequiredLength
             || chars.Distinct().Count() < _passwordOptions.RequiredUniqueChars;
             i++)
        {
            string rcs = randomChars[rand.Next(0, randomChars.Length)];
            chars.Insert(rand.Next(0, chars.Count),
                rcs[rand.Next(0, rcs.Length)]);
        }

        return new string(chars.ToArray());
    }
}