using Decenea.Domain.Common;
using Decenea.Domain.Entities.ApplicationUserEntities;

namespace Decenea.Application.Services.QueryServices.IQueryServices;

public interface IApplicationUserQueryService
{
    Task<Result<List<ApplicationUser>, Exception>> GetManyApplicationUsers();
}