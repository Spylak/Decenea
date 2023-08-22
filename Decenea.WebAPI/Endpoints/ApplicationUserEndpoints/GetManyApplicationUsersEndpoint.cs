using Decenea.Application.Services.QueryServices.IQueryServices;
using Decenea.Domain.Common;
using Decenea.Domain.Constants;
using Decenea.Domain.DataTransferObjects.ApplicationUser;
using Decenea.Domain.Entities.ApplicationUserEntities;

namespace Decenea.WebAPI.Endpoints.ApplicationUserEndpoints;

public class GetManyApplicationUsersEndpoint : Endpoint<GetManyApplicationUsersRequestDto, ApiResponse<List<ApplicationUser>>>
{
    private readonly IApplicationUserQueryService _applicationUserQueryService;
    public GetManyApplicationUsersEndpoint(IApplicationUserQueryService applicationUserQueryService)
    {
        _applicationUserQueryService = applicationUserQueryService;
    }
    
    public override void Configure()
    {
        Get("/ApplicationUser/GetMany");
        Roles(ApplicationRoles.SuperAdmin);
    }

    public override async Task<ApiResponse<List<ApplicationUser>>> ExecuteAsync(GetManyApplicationUsersRequestDto req, CancellationToken ct)
    {
        var result = await _applicationUserQueryService.GetManyApplicationUsers();
        return new ApiResponse<List<ApplicationUser>>(result.Value, result.IsSuccess, result.Message);
    }
}