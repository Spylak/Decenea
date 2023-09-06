using Decenea.Domain.Aggregates.ApplicationUserAggregate;

namespace Decenea.Domain.Extensions;

public static class ApplicationUserExtension
{
    public static string GetApplicationUserFullNameAndLocation(this ApplicationUser applicationUser)
    {
        var firstName = applicationUser.FirstName;
        var middleName = applicationUser.MiddleName;
        var lastName = applicationUser.LastName;
        // var city = applicationUser.City.Name;
        return $"{firstName} {middleName} {lastName} of city";
    }
}