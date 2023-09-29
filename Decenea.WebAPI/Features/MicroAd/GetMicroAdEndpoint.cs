using Decenea.Application.MicroAds.Queries.GetMicroAd;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Advertisement;
using Decenea.Common.Requests.MicroAds;
using Mediator;

namespace Decenea.WebAPI.Features.MicroAd;

public class GetMicroAdEndpoint : Endpoint<GetMicroAdRequest, ApiResponse<MicroAdDto>>
{
    private readonly IMediator _mediator;
    public GetMicroAdEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public override void Configure()
    {
        Get("/MicroAd/Get");
        AllowAnonymous();
    }
    
    public override async Task<ApiResponse<MicroAdDto>> ExecuteAsync(GetMicroAdRequest req, CancellationToken ct)
    {
        var getMicroAdQuery = new GetMicroAdQuery()
        {
            Id = req.Id
        };
        
        var result = await _mediator.Send(getMicroAdQuery, ct);
        return new ApiResponse<MicroAdDto>(result.Value, result.IsSuccess, result.Messages);
    }
}