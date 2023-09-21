using Decenea.Application.Users.Commands.RegisterUser;
using Decenea.Common.Common;
using Mediator;
using Microsoft.AspNetCore.Identity;

namespace Decenea.WebAPI.Features.User.RegisterUser;

public class RegisterApplicationUser : Endpoint<RegisterUserRequest,ApiResponse<object>>
{
    private readonly IMediator _mediator;
    public RegisterApplicationUser(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("/ApplicationUser/Register");
        AllowAnonymous();
    }

    public override async Task<ApiResponse<object>> ExecuteAsync(RegisterUserRequest req, CancellationToken ct)
    {
        var command = new RegisterUserCommand(req.Email,
            req.UserName,
            req.FirstName,
            req.LastName,
            req.MiddleName,
            req.PhoneNumber,
            req.CityId,
            req.Password);
        
        var result = await _mediator.Send(command);
        
        return new ApiResponse<object>(result.Value,result.IsSuccess,result.Messages);
    }
}