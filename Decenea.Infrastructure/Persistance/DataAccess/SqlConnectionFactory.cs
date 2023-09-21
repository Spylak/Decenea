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

    public async Task<IDbConnection> CreateConnectionAsync()
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        return connection;
    }
    public IDbConnection CreateConnection()
    {
        var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        return connection;
    }
}