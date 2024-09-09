using System.Security.Cryptography;
using System.Text;
using ErrorOr;
using Microsoft.AspNetCore.Identity;

namespace Decenea.Application.Helpers;

public class PasswordHelper
{
    private readonly PasswordOptions _passwordOptions;
    private const int _keySize = 64; 
    private const int _iterations = 600000;
    private readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;
    private readonly byte[]? _pepper;

    public PasswordHelper()
    {
        _passwordOptions = new PasswordOptions()
        {
            RequiredLength = 8,
            RequiredUniqueChars = 4,
            RequireDigit = true,
            RequireLowercase = true,
            RequireNonAlphanumeric = true,
            RequireUppercase = true
        };
    }
    
    public PasswordHelper(byte[] pepper, PasswordOptions? passwordOptions = null)
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

        _pepper = pepper;
    }

    public string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(_keySize);
        byte[] hash = HashPasswordWithPbkdf2(password, salt);

        byte[] hashBytes = new byte[_keySize * 2];
        Array.Copy(salt, 0, hashBytes, 0, _keySize);
        Array.Copy(hash, 0, hashBytes, _keySize, _keySize);

        return Convert.ToBase64String(hashBytes);
    }

    public bool VerifyPassword(string password, string storedHash)
    {
        byte[] hashBytes = Convert.FromBase64String(storedHash);
        byte[] salt = new byte[_keySize];
        Array.Copy(hashBytes, 0, salt, 0, _keySize);

        byte[] computedHash = HashPasswordWithPbkdf2(password, salt);

        for (int i = 0; i < _keySize; i++)
        {
            if (hashBytes[i + _keySize] != computedHash[i])
                return false;
        }
        return true;
    }

    private byte[] HashPasswordWithPbkdf2(string password, byte[] salt)
    {
        if (_pepper is null)
        {
            throw new Exception("Pepper was not found.");
        }
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] pepperedPassword = new byte[passwordBytes.Length + _pepper.Length];
        Array.Copy(passwordBytes, pepperedPassword, passwordBytes.Length);
        Array.Copy(_pepper, 0, pepperedPassword, passwordBytes.Length, _pepper.Length);

        return Rfc2898DeriveBytes.Pbkdf2(
            pepperedPassword,
            salt,
            _iterations,
            _hashAlgorithm,
            _keySize);
    }

    public ErrorOr<object> ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return Error.Failure(description: "Password cannot be empty or whitespace.");

        if (password.Length < _passwordOptions.RequiredLength)
            return Error.Failure(description: $"Password must be at least {_passwordOptions.RequiredLength} characters long.");

        if (password.Distinct().Count() < _passwordOptions.RequiredUniqueChars)
            return Error.Failure(description: $"Password must have at least {_passwordOptions.RequiredUniqueChars} unique characters.");

        if (_passwordOptions.RequireDigit && !password.Any(char.IsDigit))
            return Error.Failure(description: "Password must contain at least one digit.");

        if (_passwordOptions.RequireLowercase && !password.Any(char.IsLower))
            return Error.Failure(description: "Password must contain at least one lowercase letter.");

        if (_passwordOptions.RequireUppercase && !password.Any(char.IsUpper))
            return Error.Failure(description: "Password must contain at least one uppercase letter.");

        if (_passwordOptions.RequireNonAlphanumeric && password.All(char.IsLetterOrDigit))
            return Error.Failure(description: "Password must contain at least one non-alphanumeric character.");

        return "Password is valid.";
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

        using var rng = RandomNumberGenerator.Create();
        List<char> chars = new List<char>();

        if (_passwordOptions.RequireUppercase)
            chars.Insert(GetNextRandom(rng, chars.Count),
                randomChars[0][GetNextRandom(rng, randomChars[0].Length)]);

        if (_passwordOptions.RequireLowercase)
            chars.Insert(GetNextRandom(rng, chars.Count),
                randomChars[1][GetNextRandom(rng, randomChars[1].Length)]);

        if (_passwordOptions.RequireDigit)
            chars.Insert(GetNextRandom(rng, chars.Count),
                randomChars[2][GetNextRandom(rng, randomChars[2].Length)]);

        if (_passwordOptions.RequireNonAlphanumeric)
            chars.Insert(GetNextRandom(rng, chars.Count),
                randomChars[3][GetNextRandom(rng, randomChars[3].Length)]);

        for (int i = chars.Count;
             i < _passwordOptions.RequiredLength
             || chars.Distinct().Count() < _passwordOptions.RequiredUniqueChars;
             i++)
        {
            string rcs = randomChars[GetNextRandom(rng, randomChars.Length)];
            chars.Insert(GetNextRandom(rng, chars.Count),
                rcs[GetNextRandom(rng, rcs.Length)]);
        }

        return new string(chars.ToArray());
    }

    private int GetNextRandom(RandomNumberGenerator rng, int max)
    {
        byte[] randomBytes = new byte[4];
        rng.GetBytes(randomBytes);
        return (int)(BitConverter.ToUInt32(randomBytes, 0) % max);
    }
}