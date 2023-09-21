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
}