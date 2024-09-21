using Decenea.Application.Features.User.Queries.GetManyUsers;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.User;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;


namespace Decenea.WebAPI.Endpoints.User;

public class GetManyUsersEndpoint : Endpoint<EmptyRequest, ApiResponseResult<List<UserDto>>>
{
    public override void Configure()
    {
        Get("/users/get-many");
        Roles(nameof(UserRole.Admin));
    }

    public override async Task<ApiResponseResult<List<UserDto>>> ExecuteAsync(EmptyRequest req, CancellationToken ct)
    {
        var result = await new GetManyUsersQuery().ExecuteAsync(ct);
        return new ApiResponseResult<List<UserDto>>(result.Value , result.IsError, result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}