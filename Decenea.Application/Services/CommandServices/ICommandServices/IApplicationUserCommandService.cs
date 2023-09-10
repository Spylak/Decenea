using Decenea.Domain.Common;
using Decenea.Shared.DataTransferObjects.Auth;

namespace Decenea.Application.Services.CommandServices.ICommandServices;

public interface IApplicationUserCommandService
{
    Task<Result<RegenerateAuthTokensResponseDto, Exception>> RegenerateAuthTokens(RegenerateAuthTokensRequestDto requestDto);
}