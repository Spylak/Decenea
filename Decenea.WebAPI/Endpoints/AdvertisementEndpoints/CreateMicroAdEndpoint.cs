using Decenea.Application.Extensions;
using Decenea.Application.Helpers;
using Decenea.Application.Services.CommandServices.ICommandServices;
using Decenea.Domain.Common;
using Decenea.Domain.Constants;
using Decenea.Domain.DataTransferObjects.Advertisement;
using Decenea.Domain.DataTransferObjects.Auth;

namespace Decenea.WebAPI.Endpoints.AdvertisementEndpoints;

public class CreateMicroAdEndpoint : Endpoint<CreateMicroAdRequestDto, ApiResponse<object>>
{
    private readonly IAdvertisementCommandService _advertisementCommandService;
    public CreateMicroAdEndpoint(IAdvertisementCommandService advertisementCommandService)
    {
        _advertisementCommandService = advertisementCommandService;
    }
    
    public override void Configure()
    {
        Post("/MicroAd/Create");
        Roles(ApplicationRoles.SuperAdmin,ApplicationRoles.Admin,ApplicationRoles.Member);
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
        return new ApiResponse<object>(result.Value, result.IsSuccess, result.Message);
    }
}