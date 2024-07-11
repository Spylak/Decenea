using Decenea.Application.Users.Commands.LoginUser;
using Decenea.Common.DataTransferObjects.Auth;
using Decenea.Common.DataTransferObjects.User;
using Decenea.Domain.Aggregates.UserAggregate;

namespace Decenea.Application.Mappers;

public static class UserMapper
{
    public static void UserToLoginUserDto(this User user,
        LoginUserResponse loginApplicationUserResponseDto)
    {
        loginApplicationUserResponseDto.Email = user.Email;
        loginApplicationUserResponseDto.FirstName = user.FirstName;
        loginApplicationUserResponseDto.LastName = user.LastName;
        loginApplicationUserResponseDto.MiddleName = user.MiddleName;
    }
    public static UserDto UserToUserDto(this User user,
        UserDto? userDto = null)
    {
        userDto ??= new UserDto();
        userDto.Id = user.Id;
        userDto.Email = user.Email;
        userDto.UserName = user.UserName;
        userDto.FirstName = user.FirstName;
        userDto.LastName = user.LastName;
        userDto.MiddleName = user.MiddleName;
        userDto.PhoneNumber = user.PhoneNumber;
        return userDto;
    }
}