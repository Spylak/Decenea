using System.Data;

namespace Decenea.Application.Abstractions.DataAccess;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}