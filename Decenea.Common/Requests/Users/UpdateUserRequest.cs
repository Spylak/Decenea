using Decenea.Common.Requests.Common;

namespace Decenea.Common.Requests.Users;

public class UpdateUserRequest : UpdateRequest
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string PhoneNumber { get; set; }
    public string CityId { get; set; }
}