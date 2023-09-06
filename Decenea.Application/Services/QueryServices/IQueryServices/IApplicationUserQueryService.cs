using Decenea.Domain.Aggregates.ApplicationUserAggregate;
using Decenea.Domain.Common;

namespace Decenea.Application.Services.QueryServices.IQueryServices;

public interface IApplicationUserQueryService
{
    Task<Result<List<ApplicationUser>, Exception>> GetManyApplicationUsers();
}