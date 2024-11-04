using Decenea.Application.Features.Question.Commands.CreateQuestions;
using Decenea.Common.Common;
using Decenea.Common.Constants;
using Decenea.Common.DataTransferObjects.Question;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Question;

namespace Decenea.WebAPI.Endpoints.Question;

public class CreateQuestionsEndpoint : Endpoint<CreateQuestionsRequest, ApiResponseResult<List<QuestionDto>>>
{
    public override void Configure()
    {
        Post(RouteConstants.QuestionsCreate);
        Roles(nameof(UserRole.SuperAdmin),
            nameof(UserRole.Admin),
            nameof(UserRole.Member));
    }
    
    public override async Task<ApiResponseResult<List<QuestionDto>>> ExecuteAsync(CreateQuestionsRequest req, CancellationToken ct)
    {
        var userId = HttpContext.User.FindFirst("userId")?.Value;
        
        if(userId is null)
            return new ApiResponseResult<List<QuestionDto>>(null, true, "Invalid JWT.");
        
        var result = await new CreateQuestionsCommand()
        {
            UserId = userId,
            TestId = req.TestId,
            Questions = req.Questions
        }.ExecuteAsync(ct);
        
        return new ApiResponseResult<List<QuestionDto>>(result.Value, result.IsError, result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}