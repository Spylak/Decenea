using Decenea.Application.Services.QueryServices.IQueryServices;
using Decenea.Domain.Aggregates.UserAggregate;
using Decenea.Shared.Common;
using Decenea.Shared.DataTransferObjects.ApplicationUser;

namespace Decenea.WebAPI.Features.User.GetUsers;

public class GetUsersEndpoint : Endpoint<GetUsersRequest, ApiResponse<List<object>>>
{
    private readonly IApplicationUserQueryService _applicationUserQueryService;
    public GetUsersEndpoint(IApplicationUserQueryService applicationUserQueryService)
    {
        _applicationUserQueryService = applicationUserQueryService;
    }
    
    public override void Configure()
    {
        Get("/ApplicationUser/GetMany");
        Roles(Role.AllowAny());
    }

    public override async Task<ApiResponse<List<object>>> ExecuteAsync(GetUsersRequest req, CancellationToken ct)
    {
        var result = await _applicationUserQueryService.GetManyApplicationUsers();
        return new ApiResponse<List<object>>(null, result.IsSuccess, result.Messages);
    }
}