using Decenea.Application.Users.Queries.GetManyUsers;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.User;
using Decenea.Common.Enums;
using Decenea.Common.Requests.User;
using Decenea.Domain.Aggregates.UserAggregate;


namespace Decenea.WebAPI.Features.User;

public class GetManyUsersEndpoint : Endpoint<EmptyRequest, ApiResponse<List<UserDto>>>
{
    public override void Configure()
    {
        Get("/users/get-many");
        Roles(nameof(UserRole.Admin));
    }

    public override async Task<ApiResponse<List<UserDto>>> ExecuteAsync(EmptyRequest req, CancellationToken ct)
    {
        var result = await new GetManyUsersQuery().ExecuteAsync(ct);
        return new ApiResponse<List<UserDto>>(result.SuccessValue , result.IsSuccess, result.Messages);
    }
}