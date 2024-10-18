using Decenea.Common.Common;
using Decenea.Common.Constants;
using Decenea.Common.DataTransferObjects.Auth;
using Decenea.Common.Requests.User;
using Refit;

namespace Decenea.Common.Apis;
[Headers("Content-Type: application/json")]
public interface IAuthApi
{
    [Put(RouteConstants.UsersLogin)]
    Task<ApiResponseResult<LoginUserResponse>> Login([Body] LoginUserRequest request);

    [Put(RouteConstants.UsersLogout)]
    Task Logout();

    [Put(RouteConstants.UsersRegenerateAuthTokens)]
    Task<ApiResponseResult<AuthTokensResponse>> RefreshToken([Body] RegenerateAuthTokensRequest request);
}