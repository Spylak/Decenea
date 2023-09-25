using Decenea.Common.Common;
using Mediator;

namespace Decenea.Application.MicroAds.Commands.CreateMicroAd;

public class CreateMicroAdCommand : ICommand<Result<object,Exception>>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ContactPhone { get; set; }
    public string ContactEmail { get; set; }
    public string UserId { get; set; }
    public string CityId { get; set; }
}