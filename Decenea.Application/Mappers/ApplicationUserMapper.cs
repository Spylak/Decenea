using Decenea.Application.Advertisements.Commands.LoginUser;
using Decenea.Domain.Aggregates.UserAggregate;

namespace Decenea.Application.Mappers;

public static class ApplicationUserMapper
{
    public static void ApplicationUserToLoginApplicationUserDto(this User user,
        LoginUserResponse loginApplicationUserResponseDto)
    {
        loginApplicationUserResponseDto.Email = user.Email;
        loginApplicationUserResponseDto.FirstName = user.FirstName;
        loginApplicationUserResponseDto.LastName = user.LastName;
        loginApplicationUserResponseDto.MiddleName = user.MiddleName;
    }
}