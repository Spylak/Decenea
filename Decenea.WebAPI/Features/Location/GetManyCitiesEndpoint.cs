using Decenea.Application.Location.Queries.GetManyCities;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Location;
using Mediator;

namespace Decenea.WebAPI.Features.Location;

public class GetManyCitiesEndpoint : Endpoint<GetManyCitiesRequestDto, ApiResponse<List<CityDto>>>
{
    private readonly IMediator _mediator;
    public GetManyCitiesEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }
    public override void Configure()
    {
        Get("/City/GetMany");
        AllowAnonymous();
    }

    public override async Task<ApiResponse<List<CityDto>>> ExecuteAsync(GetManyCitiesRequestDto req, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetManyCitiesQuery());
        return new ApiResponse<List<CityDto>>(result.Value, result.IsSuccess, result.Messages);
    }
}