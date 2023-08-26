using Decenea.Domain.Common;
using Decenea.Shared.DataTransferObjects.Location;

namespace Decenea.Application.Services.QueryServices.IQueryServices;

public interface ILocationQueryService
{
    Task<Result<List<CityDto>, Exception>> GetManyCities(GetManyCitiesRequestDto requestDto);
}