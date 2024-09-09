using ErrorOr;
using Decenea.Common.DataTransferObjects.User;
using FastEndpoints;

namespace Decenea.Application.Users.Queries.GetManyUsers;

public class GetManyUsersQuery : ICommand<ErrorOr<List<UserDto>>>
{
    
}