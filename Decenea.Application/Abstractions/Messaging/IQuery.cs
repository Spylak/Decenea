using Decenea.Domain.Common;
using Mediator;

namespace Decenea.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse,Exception>>
{
}