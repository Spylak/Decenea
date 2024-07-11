using Decenea.Application.Users.Queries.GetManyUsers;
using Decenea.Common.Common;
using Decenea.Common.Requests.User;
using Decenea.Domain.Aggregates.UserAggregate;


namespace Decenea.WebAPI.Features.User;

public class GetManyUsersEndpoint : Endpoint<GetUsersRequest, ApiResponse<List<object>>>
{
    public override void Configure()
    {
        Get("/User/GetMany");
        Roles(Role.Admin.ToString());
    }

    public override async Task<ApiResponse<List<object>>> ExecuteAsync(GetUsersRequest req, CancellationToken ct)
    {
        var result = await new GetManyUsersQuery().ExecuteAsync(ct);
        return new ApiResponse<List<object>>(null, result.IsSuccess, result.Messages);
    }
}