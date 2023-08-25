using Decenea.WebAPI.Services.QueryServices.IQueryServices;
using Decenea.WebAPI.Domain.Common;
using Decenea.WebAPI.Domain.Constants;
using Decenea.Domain.DataTransferObjects.Location;

namespace Decenea.WebAPI.Features.LocationEndpoints;

public class GetManyCities : Endpoint<GetManyCitiesRequestDto, ApiResponse<List<CityDto>>>
{
    private readonly ILocationQueryService _locationQueryService;
    public GetManyCities(ILocationQueryService locationQueryService)
    {
        _locationQueryService = locationQueryService;
    }
    public override void Configure()
    {
        Get("/City/GetMany");
        AllowAnonymous();
    }

    public override async Task<ApiResponse<List<CityDto>>> ExecuteAsync(GetManyCitiesRequestDto req, CancellationToken ct)
    {
        var result = await _locationQueryService.GetManyCities(req);
        return new ApiResponse<List<CityDto>>(result.Value, result.IsSuccess, result.Message);
    }
}