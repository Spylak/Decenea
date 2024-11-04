using Decenea.Application.Features.Question.Queries.GetQuestion;
using Decenea.Common.Common;
using Decenea.Common.Constants;
using Decenea.Common.DataTransferObjects.Question;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Question;

namespace Decenea.WebAPI.Endpoints.Question;

public class GetQuestionEndpoint : Endpoint<GetQuestionRequest, ApiResponseResult<QuestionDto>>
{
    public override void Configure()
    {
        Get(RouteConstants.QuestionsGet);
        Roles(UserRoleExtensions.GetAuthorizedRoles());
    }
    
    public override async Task<ApiResponseResult<QuestionDto>> ExecuteAsync(GetQuestionRequest req, CancellationToken ct)
    {
        var userId = HttpContext.User.FindFirst("userId")?.Value;
        
        if(userId is null)
            return new ApiResponseResult<QuestionDto>(null, true, "Invalid JWT.");

        var result = await new GetQuestionQuery()
        {
            UserId = userId,
            QuestionId = req.QuestionId
        }.ExecuteAsync(ct);
        
        return new ApiResponseResult<QuestionDto>(result.Value, result.IsError, result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}