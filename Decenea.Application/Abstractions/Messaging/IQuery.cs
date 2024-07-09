namespace Decenea.Application.Abstractions.Messaging;

public interface IQuery { }
public interface IQuery<out TResult> { }
public interface IServerStreamQuery<out TResult> where TResult : class { }