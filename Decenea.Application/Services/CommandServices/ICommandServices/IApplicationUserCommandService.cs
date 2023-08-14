using Decenea.Domain.Common;
using Decenea.Domain.DataTransferObjects.ApplicationUser.LoginApplicationUser;
using Decenea.Domain.DataTransferObjects.ApplicationUser.RegisterApplicationUser;
using Microsoft.AspNetCore.Identity;

namespace Decenea.Application.Services.CommandServices.ICommandServices;

public interface IApplicationUserCommandService
{
    Task<Result<IdentityResult, Exception>> RegisterUser(RegisterApplicationUserRequest request);
    Task<Result<LoginApplicationUserDto, Exception>> LoginUser(LoginApplicationUserRequest request);
}