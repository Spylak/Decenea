using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Advertisement;
using Mediator;

namespace Decenea.Application.MicroAds.Commands.CreateMicroAd;

public class CreateMicroAdCommand : ICommand<Result<MicroAdDto,Exception>>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ContactPhone { get; set; }
    public string ContactEmail { get; set; }
    public string UserId { get; set; }
    public string CityId { get; set; }
}