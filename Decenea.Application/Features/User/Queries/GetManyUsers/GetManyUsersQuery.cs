using Decenea.Common.DataTransferObjects.User;
using ErrorOr;
using FastEndpoints;

namespace Decenea.Application.Features.User.Queries.GetManyUsers;

public class GetManyUsersQuery : ICommand<ErrorOr<List<UserDto>>>
{
    
}