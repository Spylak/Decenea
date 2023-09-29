using Decenea.Application.Abstractions.Persistance.IRepositories;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Advertisement;
using Mediator;
namespace Decenea.Application.MicroAds.Queries.GetMicroAd;

public class GetMicroAdQueryHandler : IQueryHandler<GetMicroAdQuery, Result<MicroAdDto, Exception>>
{
    private readonly IMicroAdRepository _microAdRepository;

    public GetMicroAdQueryHandler(IMicroAdRepository microAdRepository)
    {
        _microAdRepository = microAdRepository;
    }
    public async ValueTask<Result<MicroAdDto, Exception>> Handle(GetMicroAdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var microAdDtos = await _microAdRepository
                .GetMicroAdDtoByMicroAdId(query.Id);

            return Result<MicroAdDto, Exception>.Anticipated(microAdDtos);
        }
        catch (Exception ex)
        {
            return Result<MicroAdDto, Exception>
                .Excepted(ex, "Didn't manage to get Micro Ad.");
        }
    }
}