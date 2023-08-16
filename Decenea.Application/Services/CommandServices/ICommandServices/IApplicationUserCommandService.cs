using Decenea.Domain.Common;
using Decenea.Domain.DataTransferObjects.ApplicationUser;
using Microsoft.AspNetCore.Identity;

namespace Decenea.Application.Services.CommandServices.ICommandServices;

public interface IApplicationUserCommandService
{
    Task<Result<IdentityResult, Exception>> RegisterUser(RegisterApplicationUserRequestDto requestDto);
    Task<Result<LoginApplicationUserDto, Exception>> LoginUser(LoginApplicationUserRequestDto requestDto);
}