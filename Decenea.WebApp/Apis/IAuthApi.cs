using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Auth;
using Decenea.Common.DataTransferObjects.User;
using Decenea.Common.Requests.User;
using Refit;

namespace Decenea.WebApp.Apis;

public interface IAuthApi
{
    [Put("/api/auth/login")]
    Task<ApiResponseResult<LoginUserResponse>> Login([Body] LoginUserRequest request);

    [Put("/api/auth/logout")]
    Task Logout();

    [Put("/api/auth/regenerate-auth-tokens")]
    Task<ApiResponseResult<RegenerateAuthTokensResponse>> RefreshToken([Body] RegenerateAuthTokensRequest request);
}