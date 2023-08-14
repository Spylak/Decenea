using Decenea.Application.Services.CommandServices.ICommandServices;
using Decenea.Domain.DataTransferObjects.ApplicationUser.LoginApplicationUser;

namespace Decenea.WebAPI.Endpoints.ApplicationUserEndpoints;

public class LoginApplicationUserEndpoint : Endpoint<LoginApplicationUserRequest, LoginApplicationUserResponse>
{
    private readonly IApplicationUserCommandService _applicationUserCommandService;

    public LoginApplicationUserEndpoint(IApplicationUserCommandService applicationUserCommandService)
    {
        _applicationUserCommandService = applicationUserCommandService;
    }

    public override void Configure()
    {
        Put("/ApplicationUser/Login");
        AllowAnonymous();
    }

    public override async Task<LoginApplicationUserResponse> ExecuteAsync(LoginApplicationUserRequest req,
        CancellationToken ct)
    {
        var response = new LoginApplicationUserResponse();
        var result = await _applicationUserCommandService.LoginUser(req);
        
        response.Data = result.Value;
        response.IsSuccess = result.IsSuccess;
        if(result.IsError)
            response.Messages.Add("Did not manage to log in.");
        
        return response;
    }
}