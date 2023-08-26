using Decenea.Domain.Common;
using Decenea.Shared.DataTransferObjects.ApplicationUser;
using Decenea.Shared.DataTransferObjects.Auth;
using Microsoft.AspNetCore.Identity;

namespace Decenea.Application.Services.CommandServices.ICommandServices;

public interface IApplicationUserCommandService
{
    Task<Result<IdentityResult, Exception>> RegisterUser(RegisterApplicationUserRequestDto requestDto);
    Task<Result<LoginApplicationUserResponseDto, Exception>> LoginUser(LoginApplicationUserRequestDto requestDto);
    Task<Result<RegenerateAuthTokensResponseDto, Exception>> RegenerateAuthTokens(RegenerateAuthTokensRequestDto requestDto);
}