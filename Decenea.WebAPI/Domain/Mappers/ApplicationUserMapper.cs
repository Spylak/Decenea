using Decenea.Domain.DataTransferObjects.ApplicationUser;
using Decenea.Domain.Entities.ApplicationUserEntities;

namespace Decenea.WebAPI.Domain.Mappers;

public static class ApplicationUserMapper
{
    public static void ApplicationUserToLoginApplicationUserDto(this ApplicationUser applicationUser,
        LoginApplicationUserResponseDto loginApplicationUserResponseDto)
    {
        loginApplicationUserResponseDto.Email = applicationUser.Email ?? applicationUser.UserName;
        loginApplicationUserResponseDto.FirstName = applicationUser.FirstName;
        loginApplicationUserResponseDto.LastName = applicationUser.LastName;
        loginApplicationUserResponseDto.MiddleName = applicationUser.MiddleName;
        loginApplicationUserResponseDto.ResidenceOf = applicationUser.ResidenceOf;
    }
}