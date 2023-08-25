using Decenea.WebAPI.Domain.Common;
using Decenea.Domain.DataTransferObjects.Location;

namespace Decenea.WebAPI.Services.QueryServices.IQueryServices;

public interface ILocationQueryService
{
    Task<Result<List<CityDto>, Exception>> GetManyCities(GetManyCitiesRequestDto requestDto);
}