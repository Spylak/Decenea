using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Decenea.Common.Common;
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
            .FirstOrDefault(i => i.Key == "email")?
            .Value;
    }
    
    public static string? GetClaimValueByKey(this List<ClaimJwt> listClaims,string key)
    {
        return listClaims
            .FirstOrDefault(i => i.Key == key)?
            .Value;
    }
    
    public static Result<string,Exception> GetClaimValueByKey(this string jwtString, string key)
    {
        var listClaims = jwtString.GetTokenClaimJwts();
        if (!listClaims.IsSuccess)
            return Result<string,Exception>.Anticipated(null, listClaims.Messages);
        
        var claim = listClaims.SuccessValue?.FirstOrDefault(c => c.Key == key);
        if(claim is null)
            return Result<string,Exception>.Anticipated(null, [$"No value found for key {key}"]);
 
        return Result<string,Exception>.Anticipated(claim.Value);
    }

    public static Result<bool?,Exception> IsJwtTokenExpired(this string jwtString)
    {
        var expDate = jwtString.GetClaimValueByKey("exp");
        if (!expDate.IsSuccess)
        {
            return Result<bool?, Exception>.Anticipated(null, expDate.Messages);
        }
        var expirationDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expDate.SuccessValue)).UtcDateTime;

        return Result<bool?, Exception>.Anticipated(expirationDate < DateTime.UtcNow);
    }

    public static Result<IEnumerable<Claim>, Exception> GetTokenClaims(this string jwtString)
    {
        if (string.IsNullOrWhiteSpace(jwtString))
            return Result<IEnumerable<Claim>, Exception>.Anticipated(null, ["The given token string is empty or null."]);
            
        var handler = new JwtSecurityTokenHandler();
        if (!handler.CanReadToken(jwtString))
            return Result<IEnumerable<Claim>, Exception>.Anticipated(null, ["The given token string cannot be read"]);

        var jwtToken = handler.ReadJwtToken(jwtString);
        return Result<IEnumerable<Claim>, Exception>.Anticipated(jwtToken.Claims);
    }
    public static Result<List<ClaimJwt>,Exception> GetTokenClaimJwts(this string jwtString)
    {
        try
        {
            var claims = jwtString.GetTokenClaims();
            if(!claims.IsSuccess)
                return Result<List<ClaimJwt>,Exception>.Anticipated(null, claims.Messages);
            
            var claimJwts = new List<ClaimJwt>();
            
            foreach (var claim in claims.SuccessValue)
            {
                claimJwts.Add(new ClaimJwt(claim.Type,claim.Value));
            }

            return Result<List<ClaimJwt>,Exception>.Anticipated(claimJwts);
        }
        catch (Exception e)
        {
            return Result<List<ClaimJwt>,Exception>
                .Excepted(e,[$"Didn't manage to get values from token: {jwtString}"]);
        }
    }
}