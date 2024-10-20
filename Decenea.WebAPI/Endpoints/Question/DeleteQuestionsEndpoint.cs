using Decenea.Application.Features.Question.Commands.DeleteQuestion;
using Decenea.Common.Common;
using Decenea.Common.Constants;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Question;

namespace Decenea.WebAPI.Endpoints.Question;

public class DeleteQuestionsEndpoint : Endpoint<DeleteQuestionsRequest, ApiResponseResult<object>>
{
    public override void Configure()
    {
        Get(RouteConstants.QuestionsGetMany);
        Roles(UserRoleExtensions.GetAuthorizedRoles());
    }
    
    public override async Task<ApiResponseResult<object>> ExecuteAsync(DeleteQuestionsRequest req, CancellationToken ct)
    {
        var userId = HttpContext.User.FindFirst("userId")?.Value;
        
        if(userId is null)
            return new ApiResponseResult<object>(null, true, "Invalid JWT.");

        var result = await new DeleteQuestionsCommand()
        {
            UserId = userId,
            QuestionIds = req.QuestionIds
        }.ExecuteAsync(ct);
        
        return new ApiResponseResult<object>(result.Value, result.IsError, result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}