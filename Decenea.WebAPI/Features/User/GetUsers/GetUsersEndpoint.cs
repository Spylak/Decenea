using Decenea.Application.Users.Queries.GetManyUsers;
using Decenea.Domain.Aggregates.UserAggregate;
using Decenea.Common.Common;
using Decenea.Common.Requests.Users;
using Mediator;

namespace Decenea.WebAPI.Features.User.GetUsers;

public class GetUsersEndpoint : Endpoint<GetUsersRequest, ApiResponse<List<object>>>
{
    private readonly IMediator _mediator;
    public GetUsersEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public override void Configure()
    {
        Get("/ApplicationUser/GetMany");
        Roles(Role.AllowAny());
    }

    public override async Task<ApiResponse<List<object>>> ExecuteAsync(GetUsersRequest req, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetManyUsersQuery());
        return new ApiResponse<List<object>>(null, result.IsSuccess, result.Messages);
    }
}