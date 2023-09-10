using Decenea.Domain.Aggregates.UserAggregate;
using Decenea.Domain.Common;

namespace Decenea.Application.Services.QueryServices.IQueryServices;

public interface IApplicationUserQueryService
{
    Task<Result<List<User>, Exception>> GetManyApplicationUsers();
}