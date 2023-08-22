using Decenea.Domain.Common;
using Decenea.Domain.DataTransferObjects.Location;

namespace Decenea.Application.Services.QueryServices.IQueryServices;

public interface ILocationQueryService
{
    Task<Result<List<CityDto>, Exception>> GetManyCities(GetManyCitiesRequestDto requestDto);
}