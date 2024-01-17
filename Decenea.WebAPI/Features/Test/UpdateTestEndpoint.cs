using Decenea.Application.Tests.Commands.UpdateTest;
using Decenea.Common.Common;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Test;
using Decenea.Domain.Aggregates.UserAggregate;
using Decenea.Domain.Helpers;


namespace Decenea.WebAPI.Features.Test;

public class UpdateTestEndpoint : Endpoint<UpdateTestRequest, ApiResponse<object>>
{
    private readonly UpdateTestCommandHandler _updateTestCommandHandler;
    public UpdateTestEndpoint(UpdateTestCommandHandler updateTestCommandHandler)
    {
        _updateTestCommandHandler = updateTestCommandHandler;
    }
    
    public override void Configure()
    {
        Put("/Test/Update");
        Roles(Role.RoleName(Role.SuperAdmin),
            Role.RoleName(Role.Admin),
            Role.RoleName(Role.Member));
    }
    
    public override async Task<ApiResponse<object>> ExecuteAsync(UpdateTestRequest req, CancellationToken ct)
    {

        var accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ","");

        var claims = accessToken.GetTokenClaimJwts();;

        var userId = claims.Value?.GetClaimValueByKey("userId");   
        var cityId = claims.Value?.GetClaimValueByKey("cityId");
        
        if(userId is null || cityId is null)
            return new ApiResponse<object>(null, false, "Invalid JWT.");
        
        var updateTestCommand = new UpdateTestCommand()
        {
            Id = req.Id,
            CityId = cityId,
            Title = req.Title,
            ContactPhone = req.ContactPhone,
            ContactEmail = req.ContactEmail,
            Description = req.Description,
            Version = req.Version
        };
        
        var result = await _updateTestCommandHandler.Handle(updateTestCommand, ct);
        return new ApiResponse<object>(result.Value, result.IsSuccess, result.Messages);
    }
}