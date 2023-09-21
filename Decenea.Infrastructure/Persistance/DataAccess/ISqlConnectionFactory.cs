using System.Data;

namespace Decenea.Application.Abstractions.DataAccess;

public interface ISqlConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync();
    IDbConnection CreateConnection();
}