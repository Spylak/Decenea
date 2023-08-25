using Decenea.WebAPI.Domain.Common;
using Decenea.Domain.DataTransferObjects.Advertisement;

namespace Decenea.WebAPI.Services.CommandServices.ICommandServices;

public interface IAdvertisementCommandService
{
    Task<Result<object, Exception>> CreateMicroAd(CreateMicroAdServiceRequestDto requestDto);
}