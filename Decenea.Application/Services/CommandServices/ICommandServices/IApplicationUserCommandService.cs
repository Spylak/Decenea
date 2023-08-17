using Decenea.Domain.Common;
using Decenea.Domain.DataTransferObjects.ApplicationUser;
using Decenea.Domain.DataTransferObjects.Auth;
using Microsoft.AspNetCore.Identity;

namespace Decenea.Application.Services.CommandServices.ICommandServices;

public interface IApplicationUserCommandService
{
    Task<Result<IdentityResult, Exception>> RegisterUser(RegisterApplicationUserRequestDto requestDto);
    Task<Result<LoginApplicationUserResponseDto, Exception>> LoginUser(LoginApplicationUserRequestDto requestDto);
    Task<Result<RegenerateAuthTokensResponseDto, Exception>> RegenerateAuthTokens(RegenerateAuthTokensRequestDto requestDto);
}