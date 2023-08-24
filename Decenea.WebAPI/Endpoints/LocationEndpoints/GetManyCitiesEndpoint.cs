using Decenea.Application.Services.QueryServices.IQueryServices;
using Decenea.Domain.Common;
using Decenea.Domain.Constants;
using Decenea.Domain.DataTransferObjects.Location;

namespace Decenea.WebAPI.Endpoints.LocationEndpoints;

public class GetManyCitiesEndpoint : Endpoint<GetManyCitiesRequestDto, ApiResponse<List<CityDto>>>
{
    private readonly ILocationQueryService _locationQueryService;
    public GetManyCitiesEndpoint(ILocationQueryService locationQueryService)
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