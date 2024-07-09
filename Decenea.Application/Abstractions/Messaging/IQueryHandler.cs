using System.Runtime.CompilerServices;

namespace Decenea.Application.Abstractions.Messaging;

public interface IQueryHandler
{
}

public interface IQueryHandler<in TQuery> : IQueryHandler where TQuery : IQuery
{
    Task ExecuteAsync(TQuery query, CancellationToken ct); // = default);
}

public interface IQueryHandler<in TQuery, TResult> : IQueryHandler where TQuery : IQuery<TResult>
{
    Task<TResult> ExecuteAsync(TQuery query, CancellationToken ct); // = default);
}

public interface IServerStreamQueryHandler<in TQuery, TResult>
    where TQuery : class, IServerStreamQuery<TResult>
    where TResult : class
{
    IAsyncEnumerable<TResult> ExecuteAsync(TQuery query, [EnumeratorCancellation] CancellationToken ct);
}

public interface IClientStreamQueryHandler<T, TResult>
    where T : class
    where
    TResult : class
{
    Task<TResult> ExecuteAsync(IAsyncEnumerable<T> stream, CancellationToken ct);
}