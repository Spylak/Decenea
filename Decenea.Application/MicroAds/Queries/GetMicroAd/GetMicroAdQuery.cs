using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Advertisement;
using Mediator;

namespace Decenea.Application.MicroAds.Queries.GetMicroAd;

public class GetMicroAdQuery : IQuery<Result<MicroAdDto,Exception>>
{
    public required string Id { get; set; }
}