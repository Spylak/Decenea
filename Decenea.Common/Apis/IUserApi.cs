using Decenea.Common.Common;
using Decenea.Common.Constants;
using Decenea.Common.DataTransferObjects.User;
using Decenea.Common.Requests.User;
using Refit;

namespace Decenea.Common.Apis;
[Headers("Content-Type: application/json")]
public interface IUserApi
{
    [Post(RouteConstants.UsersRegister)]
    [Headers("AllowAnonymous: true")]
    Task<ApiResponseResult<UserDto>> Register([Body] RegisterUserRequest request);
}