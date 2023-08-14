using Decenea.Application.Services.CommandServices.ICommandServices;
using Decenea.Domain.DataTransferObjects.ApplicationUser.RegisterApplicationUser;

namespace Decenea.WebAPI.Endpoints.ApplicationUserEndpoints;

public class RegisterApplicationUserEndpoint : Endpoint<RegisterApplicationUserRequest,RegisterApplicationUserResponse>
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

    public override async Task<RegisterApplicationUserResponse> ExecuteAsync(RegisterApplicationUserRequest req, CancellationToken ct)
    {
        var response = new RegisterApplicationUserResponse();
        
        var result = await _applicationUserCommandService.RegisterUser(req);
        
        if (result.Value is not null)
        {
            response.IsSuccess = result.Value.Succeeded;
            response.Messages.AddRange(result.Value.Errors.Select(j => j.Description));
        }
        else
        {
            response.IsSuccess = result.IsSuccess;
            response.Messages.Add("Did not manage to create the user.");
        }
        
        return response;
    }
}