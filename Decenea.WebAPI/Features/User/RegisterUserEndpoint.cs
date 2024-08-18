using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Users.Commands.RegisterUser;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.User;
using Decenea.Common.Requests.User;


namespace Decenea.WebAPI.Features.User;

public class RegisterUserEndpoint : Endpoint<RegisterUserRequest, ApiResponse<UserDto>>
{ 
    public override void Configure()
    {
        Post("/User/Register");
        AllowAnonymous();
    }

    public override async Task<ApiResponse<UserDto>> ExecuteAsync(RegisterUserRequest req, CancellationToken ct)
    {
        var result = await new RegisterUserCommand(req.Email,
            req.UserName,
            req.FirstName,
            req.LastName,
            req.MiddleName,
            req.PhoneNumber,
            req.Password).ExecuteAsync(ct);
        
        return new ApiResponse<UserDto>(result.SuccessValue,result.IsSuccess,result.Messages);
    }
}