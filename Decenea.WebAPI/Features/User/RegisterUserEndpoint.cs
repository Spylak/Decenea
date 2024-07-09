using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Users.Commands.RegisterUser;
using Decenea.Common.Common;
using Decenea.Common.Requests.User;


namespace Decenea.WebAPI.Features.User;

public class RegisterUserEndpoint : Endpoint<RegisterUserRequest,ApiResponse<object>>
{
    private readonly RegisterUserCommandHandler _registerUserCommandHandler;
    public RegisterUserEndpoint(RegisterUserCommandHandler registerUserCommandHandler)
    {
        _registerUserCommandHandler = registerUserCommandHandler;
    }

    public override void Configure()
    {
        Post("/User/Register");
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
            req.Password);
        
        var result = await _registerUserCommandHandler.Handle(command, ct);
        
        return new ApiResponse<object>(result.Value,result.IsSuccess,result.Messages);
    }
}