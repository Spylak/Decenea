using Decenea.Application.Features.Question.Commands.CreateQuestion;
using Decenea.Common.Common;
using Decenea.Common.Constants;
using Decenea.Common.DataTransferObjects.Question;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Question;

namespace Decenea.WebAPI.Endpoints.Question;

public class CreateQuestionEndpoint : Endpoint<CreateQuestionRequest, ApiResponseResult<QuestionDto>>
{
    public override void Configure()
    {
        Post(RouteConstants.QuestionsCreate);
        Roles(nameof(UserRole.SuperAdmin),
            nameof(UserRole.Admin),
            nameof(UserRole.Member));
    }
    
    public override async Task<ApiResponseResult<QuestionDto>> ExecuteAsync(CreateQuestionRequest req, CancellationToken ct)
    {
        var userId = HttpContext.User.FindFirst("userId")?.Value;
        
        if(userId is null)
            return new ApiResponseResult<QuestionDto>(null, true, "Invalid JWT.");
        
        var result = await new CreateQuestionCommand()
        {
            UserId = userId,
            Question = req.Question,
        }.ExecuteAsync(ct);
        
        return new ApiResponseResult<QuestionDto>(result.Value, result.IsError, result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}