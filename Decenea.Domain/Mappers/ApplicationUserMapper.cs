using Decenea.Domain.Entities.ApplicationUserEntities;
using Decenea.Shared.DataTransferObjects.ApplicationUser;

namespace Decenea.Domain.Mappers;

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