using Decenea.Application.Services.QueryServices.IQueryServices;
using Decenea.Domain.DataTransferObjects.ApplicationUser.GetManyApplicationUsers;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Decenea.WebAPI.Endpoints.ApplicationUserEndpoints;

public class GetManyApplicationUsersEndpoint : Endpoint<GetManyApplicationUsersRequest, GetManyApplicationUsersResponse>
{
    private readonly IApplicationUserQueryService _applicationUserQueryService;
    public GetManyApplicationUsersEndpoint(IApplicationUserQueryService applicationUserQueryService)
    {
        _applicationUserQueryService = applicationUserQueryService;
    }
    
    public override void Configure()
    {
        Get("/ApplicationUser/GetMany");
        Roles("Guest");
    }

    public override async Task<GetManyApplicationUsersResponse> ExecuteAsync(GetManyApplicationUsersRequest req, CancellationToken ct)
    {
        var context = HttpContext.Request.Headers["Authorization"];
        var currentUser = HttpContext.User;
        var result = await _applicationUserQueryService.GetManyApplicationUsers();
        return new GetManyApplicationUsersResponse() { Data = result.Value };
    }
}