using Decenea.Application.MicroAds.Commands.UpdateMicroAd;
using Decenea.Common.Common;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.MicroAds;
using Decenea.Domain.Aggregates.UserAggregate;
using Decenea.Domain.Helpers;
using Mediator;

namespace Decenea.WebAPI.Features.MicroAd;

public class UpdateMicroAdEndpoint : Endpoint<UpdateMicroAdRequest, ApiResponse<object>>
{
    private readonly IMediator _mediator;
    public UpdateMicroAdEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public override void Configure()
    {
        Put("/MicroAd/Update");
        Roles(Role.RoleName(Role.SuperAdmin),
            Role.RoleName(Role.Admin),
            Role.RoleName(Role.Member));
    }
    
    public override async Task<ApiResponse<object>> ExecuteAsync(UpdateMicroAdRequest req, CancellationToken ct)
    {

        var accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ","");

        var claims = AuthTokenHelper
            .GetTokenClaims(accessToken);

        var userId = claims.Value?.GetClaimValueByKey("userId");   
        var cityId = claims.Value?.GetClaimValueByKey("cityId");
        
        if(userId is null || cityId is null)
            return new ApiResponse<object>(null, false, "Invalid JWT.");
        
        var updateMicroAdCommand = new UpdateMicroAdCommand()
        {
            Id = req.Id,
            CityId = cityId,
            Title = req.Title,
            ContactPhone = req.ContactPhone,
            ContactEmail = req.ContactEmail,
            Description = req.Description,
            Version = req.Version
        };
        
        var result = await _mediator.Send(updateMicroAdCommand, ct);
        return new ApiResponse<object>(result.Value, result.IsSuccess, result.Messages);
    }
}