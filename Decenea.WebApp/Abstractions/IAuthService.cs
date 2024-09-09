using Decenea.Common.Requests.User;
using ErrorOr;

namespace Decenea.WebApp.Abstractions;

public interface IAuthService
{
    Task<ErrorOr<bool>> UserLogout();
    Task<ErrorOr<object>> Login(LoginUserRequest request);
}