using Decenea.Common.Common;
using Mediator;

namespace Decenea.Application.MicroAds.Commands.UpdateMicroAd;

public class UpdateMicroAdCommand : ICommand<Result<object,Exception>>
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ContactPhone { get; set; }
    public string ContactEmail { get; set; }
    public string CityId { get; set; }
    public string Version { get; set; }
}