using Decenea.Domain.Common;
using Mediator;

namespace Decenea.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse,Exception>>
    where TQuery : IQuery<TResponse>
{
}