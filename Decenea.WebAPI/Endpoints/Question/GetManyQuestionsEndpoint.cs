using Decenea.Application.Features.Question.Queries.GetManyQuestions;
using Decenea.Common.Common;
using Decenea.Common.Constants;
using Decenea.Common.DataTransferObjects.Question;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;

namespace Decenea.WebAPI.Endpoints.Question;

public class GetManyQuestionsEndpoint : Endpoint<EmptyRequest, ApiResponseResult<IEnumerable<QuestionDto>>>
{
    public override void Configure()
    {
        Get(RouteConstants.QuestionsGetMany);
        Roles(UserRoleExtensions.GetAuthorizedRoles());
    }
    
    public override async Task<ApiResponseResult<IEnumerable<QuestionDto>>> ExecuteAsync(EmptyRequest req, CancellationToken ct)
    {
        var userId = HttpContext.User.FindFirst("userId")?.Value;
        
        if(userId is null)
            return new ApiResponseResult<IEnumerable<QuestionDto>>(null, true, "Invalid JWT.");

        var result = await new GetManyQuestionsQuery()
        {
            UserId = userId
        }.ExecuteAsync(ct);
        
        return new ApiResponseResult<IEnumerable<QuestionDto>>(result.Value, result.IsError, result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}