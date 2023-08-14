using Decenea.Domain.DataTransferObjects.ApplicationUser.LoginApplicationUser;
using Decenea.Domain.Entities.ApplicationUser;

namespace Decenea.Application.Mappers;

public static class ApplicationUserMapper
{
    public static void ApplicationUserToLoginApplicationUserDto(this ApplicationUser applicationUser,
        LoginApplicationUserDto loginApplicationUserDto)
    {
        loginApplicationUserDto.Email = applicationUser.Email ?? applicationUser.UserName;
        loginApplicationUserDto.FirstName = applicationUser.FirstName;
        loginApplicationUserDto.LastName = applicationUser.LastName;
        loginApplicationUserDto.MiddleName = applicationUser.MiddleName;
        loginApplicationUserDto.ResidenceOf = applicationUser.ResidenceOf;
    }
}