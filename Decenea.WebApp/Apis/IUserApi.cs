using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.User;
using Decenea.Common.Requests.User;
using Refit;

namespace Decenea.WebApp.Apis;
[Headers("Content-Type: application/json")]
public interface IUserApi
{
    
    [Post("/api/users/register")]
    Task<ApiResponseResult<UserDto>> Register([Body] RegisterUserRequest request);
}