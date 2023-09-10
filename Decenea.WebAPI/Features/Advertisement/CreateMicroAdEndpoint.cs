using Decenea.Domain.Helpers;
using Decenea.Application.Services.CommandServices.ICommandServices;
using Decenea.Domain.Aggregates.UserAggregate;
using Decenea.Shared.Common;
using Decenea.Shared.DataTransferObjects.Advertisement;
using Decenea.Shared.Extensions;

namespace Decenea.WebAPI.Features.Advertisement;

public class CreateMicroAd : Endpoint<CreateMicroAdRequestDto, ApiResponse<object>>
{
    private readonly IAdvertisementCommandService _advertisementCommandService;
    public CreateMicroAd(IAdvertisementCommandService advertisementCommandService)
    {
        _advertisementCommandService = advertisementCommandService;
    }
    
    public override void Configure()
    {
        Post("/MicroAd/Create");
        Roles(Role.SuperAdmin.ToString(),Role.Admin.ToString(),Role.Member.ToString());
    }
    
    public override async Task<ApiResponse<object>> ExecuteAsync(CreateMicroAdRequestDto req, CancellationToken ct)
    {
        var createMicroAdServiceRequestDto = new CreateMicroAdServiceRequestDto(req);

        var accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ","");

        var claims = AuthTokenHelper
            .GetTokenClaims(accessToken);

        var userId = claims.Value?.GetClaimValueByKey("UserId");   
        var cityId = claims.Value?.GetClaimValueByKey("CityId");
        
        if(userId is null || cityId is null)
            return new ApiResponse<object>(null, false, "Invalid JWT.");
        
        createMicroAdServiceRequestDto.ApplicationUserId = long.Parse(userId);
        createMicroAdServiceRequestDto.CityId = cityId;
        
        var result = await _advertisementCommandService.CreateMicroAd(createMicroAdServiceRequestDto);
        return new ApiResponse<object>(result.Value, result.IsSuccess, result.Messages);
    }
}