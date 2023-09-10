using Decenea.Domain.Aggregates.UserAggregate;

namespace Decenea.Domain.Extensions;

public static class ApplicationUserExtension
{
    public static string GetApplicationUserFullNameAndLocation(this User user)
    {
        var firstName = user.FirstName;
        var middleName = user.MiddleName;
        var lastName = user.LastName;
        // var city = applicationUser.City.Name;
        return $"{firstName} {middleName} {lastName} of city";
    }
}