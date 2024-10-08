using System.Security.Claims;
using Decenea.Application.Features.Test.Commands.UpdateTest;
using Decenea.Common.Common;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Test;


namespace Decenea.WebAPI.Endpoints.Test;

public class UpdateTestEndpoint : Endpoint<UpdateTestRequest, ApiResponseResult<object>>
{
    public override void Configure()
    {
        Put("/tests/update");
        Roles(nameof(UserRole.SuperAdmin),
            nameof(UserRole.Admin),
            nameof(UserRole.Member));
    }
    
    public override async Task<ApiResponseResult<object>> ExecuteAsync(UpdateTestRequest req, CancellationToken ct)
    {
        var accessToken = HttpContext.Request.Headers["Authorization"]
            .ToString()
            .Replace("Bearer ","");

        var claims = accessToken.GetTokenClaimJwts();

        var userId = claims.Value?.GetClaimValueByKey("userId");
        
        var userEmail = claims.Value?.GetClaimValueByKey(ClaimTypes.Email);
        
        if(userId is null || userEmail is null)
            return new ApiResponseResult<object>(null, true, "Invalid JWT.");
        
        var result = await new UpdateTestCommand()
        {
            Id = req.Id,
            Title = req.Title,
            Description = req.Description,
            Version = req.Version,
            UserId = userId,
            UserEmail = userEmail
        }.ExecuteAsync(ct);
        
        return new ApiResponseResult<object>(result.Value, result.IsError, result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}