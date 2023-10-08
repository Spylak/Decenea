using Decenea.Application.MicroAds.Commands.CreateMicroAd;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Advertisement;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.MicroAds;
using Decenea.Domain.Aggregates.UserAggregate;
using Decenea.Domain.Helpers;
using Mediator;

namespace Decenea.WebAPI.Features.MicroAd;

public class CreateMicroAdEndpoint : Endpoint<CreateMicroAdRequest, ApiResponse<MicroAdDto>>
{
    private readonly IMediator _mediator;
    public CreateMicroAdEndpoint(IMediator mediator)
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
    
    public override async Task<ApiResponse<MicroAdDto>> ExecuteAsync(CreateMicroAdRequest req, CancellationToken ct)
    {

        var accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ","");

        var claims = accessToken.GetTokenClaimJwts();

        var userId = claims.Value?.GetClaimValueByKey("userId");   
        var cityId = claims.Value?.GetClaimValueByKey("cityId");
        
        if(userId is null || cityId is null)
            return new ApiResponse<MicroAdDto>(null, false, "Invalid JWT.");
        
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
        return new ApiResponse<MicroAdDto>(result.Value, result.IsSuccess, result.Messages);
    }
}