using Decenea.Domain.Common;
using Mediator;

namespace Decenea.Application.Abstractions.Messaging;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result<object,Exception>>
    where TCommand : ICommand
{
}

public interface ICommandHandler<in TCommand, TResponse>
    : IRequestHandler<TCommand, Result<TResponse,Exception>>
    where TCommand : ICommand<TResponse>
{
}
