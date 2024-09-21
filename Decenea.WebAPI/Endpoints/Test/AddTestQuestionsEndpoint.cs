using System.Security.Claims;
using Decenea.Application.Features.Test.Commands.AddTestQuestions;
using Decenea.Common.Common;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Test;

namespace Decenea.WebAPI.Endpoints.Test;

public class AddTestQuestionsEndpoint : Endpoint<AddQuestionsRequest, ApiResponseResult<object>>
{
    public override void Configure()
    {
        Put("/tests/add-questions");
        Roles(nameof(UserRole.SuperAdmin),
            nameof(UserRole.Admin),
            nameof(UserRole.Member));
    }
    
    public override async Task<ApiResponseResult<object>> ExecuteAsync(AddQuestionsRequest req, CancellationToken ct)
    {

        var accessToken = HttpContext.Request.Headers["Authorization"]
            .ToString()
            .Replace("Bearer ","");

        var claims = accessToken.GetTokenClaimJwts();

        var userId = claims.Value?.GetClaimValueByKey("userId");
        
        var userEmail = claims.Value?.GetClaimValueByKey(ClaimTypes.Email);
        
        if(userId is null || userEmail is null)
            return new ApiResponseResult<object>(null, true, "Invalid JWT.");

        var result = await new AddTestQuestionsCommand()
        {
            UserId = userId,
            TestId = req.TestId,
            QuestionIds = req.QuestionIds,
            UserEmail = userEmail
        }.ExecuteAsync(ct);
        
        return new ApiResponseResult<object>(result.Value, result.IsError, result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}