using Decenea.Application.Advertisements.Commands.CreateMicroAd;
using Decenea.Common.Common;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.MicroAds;
using Decenea.Domain.Aggregates.UserAggregate;
using Decenea.Domain.Helpers;
using Mediator;

namespace Decenea.WebAPI.Features.Advertisement;

public class CreateMicroAd : Endpoint<CreateMicroAdRequest, ApiResponse<object>>
{
    private readonly IMediator _mediator;
    public CreateMicroAd(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public override void Configure()
    {
        Post("/MicroAd/Create");
        Roles(Role.RoleName(Role.SuperAdmin),
            Role.RoleName(Role.Admin),
            Role.RoleName(Role.Member));
    }
    
    public override async Task<ApiResponse<object>> ExecuteAsync(CreateMicroAdRequest req, CancellationToken ct)
    {

        var accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ","");

        var claims = AuthTokenHelper
            .GetTokenClaims(accessToken);

        var userId = claims.Value?.GetClaimValueByKey("UserId");   
        var cityId = claims.Value?.GetClaimValueByKey("CityId");
        
        if(userId is null || cityId is null)
            return new ApiResponse<object>(null, false, "Invalid JWT.");
        
        var createMicroAdCommand = new CreateMicroAdCommand()
        {
            UserId = userId,
            CityId = cityId,
            Title = req.Title,
            ContactPhone = req.ContactPhone,
            ContactEmail = req.ContactEmail,
            Description = req.Description
        };
        
        var result = await _mediator.Send(createMicroAdCommand, ct);
        return new ApiResponse<object>(result, result.IsSuccess, result.Messages);
    }
}