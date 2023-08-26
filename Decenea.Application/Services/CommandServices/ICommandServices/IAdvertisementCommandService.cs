using Decenea.Domain.Common;
using Decenea.Shared.DataTransferObjects.Advertisement;

namespace Decenea.Application.Services.CommandServices.ICommandServices;

public interface IAdvertisementCommandService
{
    Task<Result<object, Exception>> CreateMicroAd(CreateMicroAdServiceRequestDto requestDto);
}