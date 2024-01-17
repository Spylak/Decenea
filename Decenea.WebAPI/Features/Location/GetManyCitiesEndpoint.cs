using Decenea.Application.Location.Queries.GetManyCities;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Location;


namespace Decenea.WebAPI.Features.Location;

public class GetManyCitiesEndpoint : Endpoint<GetManyCitiesRequestDto, ApiResponse<List<CityDto>>>
{
    private readonly GetManyCitiesQueryHandler _getManyCitiesQueryHandler;
    public GetManyCitiesEndpoint(GetManyCitiesQueryHandler getManyCitiesQueryHandler)
    {
        _getManyCitiesQueryHandler = getManyCitiesQueryHandler;
    }
    public override void Configure()
    {
        Get("/City/GetMany");
        AllowAnonymous();
    }

    public override async Task<ApiResponse<List<CityDto>>> ExecuteAsync(GetManyCitiesRequestDto req, CancellationToken ct)
    {
        var result = await _getManyCitiesQueryHandler.Handle(new GetManyCitiesQuery(), ct);
        return new ApiResponse<List<CityDto>>(result.Value, result.IsSuccess, result.Messages);
    }
}