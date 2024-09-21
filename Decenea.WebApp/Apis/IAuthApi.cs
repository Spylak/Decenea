using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Auth;
using Decenea.Common.Requests.User;
using Refit;

namespace Decenea.WebApp.Apis;
[Headers("Content-Type: application/json")]
public interface IAuthApi
{
    [Put("/api/auth/login")]
    Task<ApiResponseResult<LoginUserResponse>> Login([Body] LoginUserRequest request);

    [Put("/api/auth/logout")]
    Task Logout();

    [Put("/api/auth/regenerate-auth-tokens")]
    Task<ApiResponseResult<AuthTokensResponse>> RefreshToken([Body] RegenerateAuthTokensRequest request);
}