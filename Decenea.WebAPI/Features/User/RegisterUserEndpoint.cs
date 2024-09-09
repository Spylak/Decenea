using Decenea.Application.Users.Commands.RegisterUser;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.User;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.User;


namespace Decenea.WebAPI.Features.User;

public class RegisterUserEndpoint : Endpoint<RegisterUserRequest, ApiResponseResult<UserDto>>
{ 
    public override void Configure()
    {
        Post("/users/register");
        AllowAnonymous();
    }

    public override async Task<ApiResponseResult<UserDto>> ExecuteAsync(RegisterUserRequest req, CancellationToken ct)
    {
        var result = await new RegisterUserCommand(req.Email,
            req.UserName,
            req.FirstName,
            req.LastName,
            req.MiddleName,
            req.PhoneNumber,
            req.Password).ExecuteAsync(ct);
        
        return new ApiResponseResult<UserDto>(result.Value,result.IsError,result.Errors.ToErrorDictionary());
    }
}