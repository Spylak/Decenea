using Decenea.Application.Services.CommandServices.ICommandServices;
using Decenea.Shared.Common;
using Decenea.Shared.DataTransferObjects.ApplicationUser;
using Microsoft.AspNetCore.Identity;

namespace Decenea.WebAPI.Features.ApplicationUser;

public class RegisterApplicationUser : Endpoint<RegisterApplicationUserRequestDto,ApiResponse<IdentityResult>>
{
    private readonly IApplicationUserCommandService _applicationUserCommandService;
    
    public RegisterApplicationUser(IApplicationUserCommandService applicationUserCommandService)
    {
        _applicationUserCommandService = applicationUserCommandService;
    }

    public override void Configure()
    {
        Post("/ApplicationUser/Register");
        AllowAnonymous();
    }

    public override async Task<ApiResponse<IdentityResult>> ExecuteAsync(RegisterApplicationUserRequestDto req, CancellationToken ct)
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
        
        return new ApiResponse<IdentityResult>(result.Value,result.IsSuccess,result.Messages);
    }
}