using Decenea.Application.Services.CommandServices.ICommandServices;
using Decenea.Domain.Common;
using Decenea.Domain.DataTransferObjects.ApplicationUser.RegisterApplicationUser;
using Microsoft.AspNetCore.Identity;

namespace Decenea.WebAPI.Endpoints.ApplicationUserEndpoints;

public class RegisterApplicationUserEndpoint : Endpoint<RegisterApplicationUserRequest,ApiResponse<IdentityResult>>
{
    private readonly IApplicationUserCommandService _applicationUserCommandService;
    
    public RegisterApplicationUserEndpoint(IApplicationUserCommandService applicationUserCommandService)
    {
        _applicationUserCommandService = applicationUserCommandService;
    }

    public override void Configure()
    {
        Post("/ApplicationUser/Register");
        AllowAnonymous();
    }

    public override async Task<ApiResponse<IdentityResult>> ExecuteAsync(RegisterApplicationUserRequest req, CancellationToken ct)
    {
        var result = await _applicationUserCommandService.RegisterUser(req);
        
        if(result.IsSuccess)
        {
            var errorMessages = result.Value?
                .Errors
                .Select(j => j.Description)
                .ToList();
            return new ApiResponse<IdentityResult>(result.Value,result.IsSuccess,errorMessages);
        }
        
        return new ApiResponse<IdentityResult>(result.Value,result.IsSuccess,result.Message);
    }
}