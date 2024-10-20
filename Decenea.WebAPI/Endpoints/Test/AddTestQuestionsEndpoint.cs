using Decenea.Application.Features.Test.Commands.AddTestQuestions;
using Decenea.Common.Common;
using Decenea.Common.Constants;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Test;

namespace Decenea.WebAPI.Endpoints.Test;

public class AddTestQuestionsEndpoint : Endpoint<AddTestQuestionsRequest, ApiResponseResult<object>>
{
    public override void Configure()
    {
        Put(RouteConstants.TestsAddQuestions);
        Roles(nameof(UserRole.SuperAdmin),
            nameof(UserRole.Admin),
            nameof(UserRole.Member));
    }
    
    public override async Task<ApiResponseResult<object>> ExecuteAsync(AddTestQuestionsRequest req, CancellationToken ct)
    {

        var accessToken = HttpContext.Request.Headers["Authorization"]
            .ToString()
            .Replace("Bearer ","");

        var claims = accessToken.GetTokenClaimJwts();

        var userId = claims.Value?.GetClaimValueByKey("userId");
        
        if(userId is null)
            return new ApiResponseResult<object>(null, true, "Invalid JWT.");

        var result = await new AddTestQuestionsCommand()
        {
            UserId = userId,
            TestId = req.TestId,
            QuestionIds = req.QuestionIds
        }.ExecuteAsync(ct);
        
        return new ApiResponseResult<object>(result.Value, result.IsError, result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}