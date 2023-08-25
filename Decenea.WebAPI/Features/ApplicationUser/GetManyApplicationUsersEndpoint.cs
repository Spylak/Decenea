using System.Text.Json.Nodes;
using Decenea.WebAPI.Services.QueryServices.IQueryServices;
using Decenea.WebAPI.Domain.Common;
using Decenea.WebAPI.Domain.Constants;
using Decenea.Domain.DataTransferObjects.ApplicationUser;
using Decenea.Domain.Entities.ApplicationUserEntities;

namespace Decenea.WebAPI.Features.ApplicationUser;

public class GetManyApplicationUsers : Endpoint<GetManyApplicationUsersRequestDto, ApiResponse<List<object>>>
{
    private readonly IApplicationUserQueryService _applicationUserQueryService;
    public GetManyApplicationUsers(IApplicationUserQueryService applicationUserQueryService)
    {
        _applicationUserQueryService = applicationUserQueryService;
    }
    
    public override void Configure()
    {
        Get("/ApplicationUser/GetMany");
        Roles(ApplicationRoles.SuperAdmin);
    }

    public override async Task<ApiResponse<List<object>>> ExecuteAsync(GetManyApplicationUsersRequestDto req, CancellationToken ct)
    {
        var result = await _applicationUserQueryService.GetManyApplicationUsers();
        return new ApiResponse<List<object>>(null, result.IsSuccess, result.Message);
    }
}