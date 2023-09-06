using Decenea.Domain.Common;
using Mediator;

namespace Decenea.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result<object,Exception>>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse,Exception>>
{
}
