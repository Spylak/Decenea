using Decenea.Common.Common;
using Decenea.Domain.Aggregates.UserAggregate;
using Mediator;

namespace Decenea.Application.Users.Queries.GetManyUsers;

public class GetManyUsersQuery : IQuery<Result<List<User>, Exception>>
{
    
}