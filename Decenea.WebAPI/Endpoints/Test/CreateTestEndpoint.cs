using Decenea.Application.Features.Test.Commands.CreateTest;
using Decenea.Common.Common;
using Decenea.Common.Constants;
using Decenea.Common.DataTransferObjects.Test;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Test;


namespace Decenea.WebAPI.Endpoints.Test;

public class CreateTestEndpoint : Endpoint<CreateTestRequest, ApiResponseResult<TestDto>>
{
    public override void Configure()
    {
        Post(RouteConstants.TestsCreate);
        Roles(nameof(UserRole.SuperAdmin),
            nameof(UserRole.Admin),
            nameof(UserRole.Member));
    }
    
    public override async Task<ApiResponseResult<TestDto>> ExecuteAsync(CreateTestRequest req, CancellationToken ct)
    {
        var userId = HttpContext.User.FindFirst("userId")?.Value;
        
        if(userId is null)
            return new ApiResponseResult<TestDto>(null, true, "Invalid JWT.");
        
        var result = await new CreateTestCommand()
        {
            Id = userId,
            UserId = userId,
            Title = req.Title,
            MinutesToComplete = req.MinutesToComplete,
            Description = req.Description,
            Questions = req.Questions
        }.ExecuteAsync(ct);
        
        return new ApiResponseResult<TestDto>(result.Value, result.IsError, result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}