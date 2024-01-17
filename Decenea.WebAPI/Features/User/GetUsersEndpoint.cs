using Decenea.Application.Users.Queries.GetManyUsers;
using Decenea.Common.Common;
using Decenea.Common.Requests.User;
using Decenea.Domain.Aggregates.UserAggregate;


namespace Decenea.WebAPI.Features.User;

public class GetManyUsersEndpoint : Endpoint<GetUsersRequest, ApiResponse<List<object>>>
{
    private readonly GetManyUsersQueryHandler _getManyUsersQueryHandler;
    public GetManyUsersEndpoint(GetManyUsersQueryHandler getManyUsersQueryHandler)
    {
        _getManyUsersQueryHandler = getManyUsersQueryHandler;
    }
    
    public override void Configure()
    {
        Get("/User/GetMany");
        Roles(Role.AllowAny());
    }

    public override async Task<ApiResponse<List<object>>> ExecuteAsync(GetUsersRequest req, CancellationToken ct)
    {
        var result = await _getManyUsersQueryHandler.Handle(new GetManyUsersQuery(), ct);
        return new ApiResponse<List<object>>(null, result.IsSuccess, result.Messages);
    }
}