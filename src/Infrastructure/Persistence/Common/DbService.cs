using Dapper;
using Microsoft.Data.SqlClient;

namespace Persistence.Common;

internal class DbService
{
    private readonly string _connectionString;
    public DbService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task ExecuteAsync(CommandDefinition command)
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();
        await connection.ExecuteAsync(command);
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(CommandDefinition command)
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();
        return await connection.QueryAsync<T>(command);
    }

    public async Task<T?> QueryFirstOrDefaultAsync<T>(CommandDefinition command)
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();
        return await connection.QueryFirstOrDefaultAsync<T>(command);
    }
}
