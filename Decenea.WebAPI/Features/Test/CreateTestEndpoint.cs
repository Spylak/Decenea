using Decenea.Application.Tests.Commands.CreateTest;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Test;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Test;
using Decenea.Domain.Aggregates.UserAggregate;
using Decenea.Domain.Helpers;


namespace Decenea.WebAPI.Features.Test;

public class CreateTestEndpoint : Endpoint<CreateTestRequest, ApiResponse<TestDto>>
{
    public override void Configure()
    {
        Post("/Test/Create");
        Roles(Role.RoleName(Role.SuperAdmin),
            Role.RoleName(Role.Admin),
            Role.RoleName(Role.Member));
    }
    
    public override async Task<ApiResponse<TestDto>> ExecuteAsync(CreateTestRequest req, CancellationToken ct)
    {

        var accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ","");

        var claims = accessToken.GetTokenClaimJwts();

        var userId = claims.Value?.GetClaimValueByKey("userId");
        
        if(userId is null)
            return new ApiResponse<TestDto>(null, false, "Invalid JWT.");
        
        var result = await new CreateTestCommand()
        {
            UserId = userId,
            Title = req.Title,
            ContactPhone = req.ContactPhone,
            ContactEmail = req.ContactEmail,
            Description = req.Description
        }.ExecuteAsync(ct);
        
        return new ApiResponse<TestDto>(result.Value, result.IsSuccess, result.Messages);
    }
}