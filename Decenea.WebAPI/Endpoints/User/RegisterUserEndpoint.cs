using Decenea.Application.Features.User.Commands.RegisterUser;
using Decenea.Common.Common;
using Decenea.Common.Constants;
using Decenea.Common.DataTransferObjects.User;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.User;


namespace Decenea.WebAPI.Endpoints.User;

public class RegisterUserEndpoint : Endpoint<RegisterUserRequest, ApiResponseResult<UserDto>>
{ 
    public override void Configure()
    {
        Post(RouteConstants.UsersRegister);
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
        
        return new ApiResponseResult<UserDto>(result.Value,result.IsError,result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}