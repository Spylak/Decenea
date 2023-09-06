using System.Data;
using Decenea.Application.Abstractions.DataAccess;
using Npgsql;

namespace Decenea.Infrastructure.Persistance.DataAccess;

internal sealed class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        return connection;
    }
}