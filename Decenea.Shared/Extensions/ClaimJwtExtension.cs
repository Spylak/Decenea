using Decenea.Shared.DataTransferObjects.Auth;

namespace Decenea.Shared.Extensions;

public static class ClaimJwtExtension
{
    public static string? GetUserNameClaimValue(this List<ClaimJwt> listClaims)
    {
        return listClaims
            .FirstOrDefault(i => i.Key == "UserName")?
            .Value;
    }
    
    public static string? GetClaimValueByKey(this List<ClaimJwt> listClaims,string key)
    {
        return listClaims
            .FirstOrDefault(i => i.Key == key)?
            .Value;
    }
}