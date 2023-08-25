using Decenea.WebAPI.Domain.Common;
using Decenea.Domain.Entities.ApplicationUserEntities;

namespace Decenea.WebAPI.Services.QueryServices.IQueryServices;

public interface IApplicationUserQueryService
{
    Task<Result<List<ApplicationUser>, Exception>> GetManyApplicationUsers();
}