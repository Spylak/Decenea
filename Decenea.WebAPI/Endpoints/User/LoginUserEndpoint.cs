using Decenea.Application.Features.User.Commands.LoginUser;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Auth;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.User;


namespace Decenea.WebAPI.Endpoints.User;

public class LoginUserEndpoint : Endpoint<LoginUserRequest, ApiResponseResult<LoginUserResponse>>
{ 
    public override void Configure()
    {
        Put("/auth/login");
        AllowAnonymous();
    }

    public override async Task<ApiResponseResult<LoginUserResponse>> ExecuteAsync(LoginUserRequest req,
        CancellationToken ct)
    {
        var result = await new LoginUserCommand(req.Email,req.Password,req.RememberMe ?? false)
            .ExecuteAsync(ct);
        return new ApiResponseResult<LoginUserResponse>(result.Value, result.IsError, result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}