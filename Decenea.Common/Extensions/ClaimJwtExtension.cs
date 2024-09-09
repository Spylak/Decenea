using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ErrorOr;
using Decenea.Common.DataTransferObjects.Auth;

namespace Decenea.Common.Extensions;

public static class ClaimJwtExtension
{
    public static string? GetUserNameClaimValue(this List<ClaimJwt> listClaims)
    {
        return listClaims
            .FirstOrDefault(i => i.Key == "userName")?
            .Value;
    }
    public static string? GetEmailClaimValue(this List<ClaimJwt> listClaims)
    {
        return listClaims
            .FirstOrDefault(i => i.Key == ClaimTypes.Email)?
            .Value;
    }
    
    public static string? GetClaimValueByKey(this List<ClaimJwt> listClaims,string key)
    {
        return listClaims
            .FirstOrDefault(i => i.Key == key)?
            .Value;
    }
    
    public static ErrorOr<string> GetClaimValueByKey(this string jwtString, string key)
    {
        var listClaims = jwtString.GetTokenClaimJwts();
        if (listClaims.IsError)
            return listClaims.Errors;
        
        var claim = listClaims.Value.FirstOrDefault(c => c.Key == key);

        return claim is null ? Error.NotFound(description: $"No value found for key {key}") : claim.Value;
    }

    public static ErrorOr<bool?> IsJwtTokenExpired(this string jwtString)
    {
        var expDate = jwtString.GetClaimValueByKey("exp");
        if (expDate.IsError)
        {
            return expDate.Errors;
        }
        var expirationDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expDate.Value)).UtcDateTime;

        return expirationDate < DateTime.UtcNow;
    }

    public static ErrorOr<List<Claim>> GetTokenClaims(this string jwtString)
    {
        if (string.IsNullOrWhiteSpace(jwtString))
            return Error.NotFound(description: "The given token string is empty or null.");
            
        var handler = new JwtSecurityTokenHandler();
        if (!handler.CanReadToken(jwtString))
            return Error.Failure(description: "The given token string cannot be read");

        var jwtToken = handler.ReadJwtToken(jwtString);
        return jwtToken.Claims.ToList();
    }
    public static ErrorOr<List<ClaimJwt>?> GetTokenClaimJwts(this string jwtString)
    {
        try
        {
            var claims = jwtString.GetTokenClaims();
            if (claims.IsError)
                return claims.Errors;
            
            var claimJwts = new List<ClaimJwt>();
            
            foreach (var claim in claims.Value)
            {
                claimJwts.Add(new ClaimJwt(claim.Type,claim.Value));
            }

            return claimJwts;
        }
        catch (Exception e)
        {
            return Error.Unexpected(description: $"Didn't manage to get values from token: {jwtString}");
        }
    }
}