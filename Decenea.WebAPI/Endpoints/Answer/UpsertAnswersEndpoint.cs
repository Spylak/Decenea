using System.Security.Claims;
using Decenea.Application.Features.Test.Commands.UpsertAnswers;
using Decenea.Common.Common;
using Decenea.Common.Constants;
using Decenea.Common.DataTransferObjects.Answer;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Answer;

namespace Decenea.WebAPI.Endpoints.Answer;

public class UpsertAnswersEndpoint : Endpoint<UpsertAnswersRequest, ApiResponseResult<List<TestAnswerDto>>>
{
    public override void Configure()
    {
        Post(RouteConstants.AnswersUpsert);
    }
    
    public override async Task<ApiResponseResult<List<TestAnswerDto>>> ExecuteAsync(UpsertAnswersRequest req, CancellationToken ct)
    {
        var userId = HttpContext.User.FindFirst("userId")?.Value;
        var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
        
        if(userId is null || userEmail is null)
            return new ApiResponseResult<List<TestAnswerDto>>(null, true, "Invalid JWT.");
        
        var result = await new UpsertAnswersCommand
        {
            UserId = userId,
            UserEmail = userEmail,
            TestId = req.TestId,
            Answers = req.Answers
        }.ExecuteAsync(ct);
        
        return new ApiResponseResult<List<TestAnswerDto>>(result.Value, result.IsError, result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}