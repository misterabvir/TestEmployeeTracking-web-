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

    public async Task ExecuteAsync(string sql, object model)
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();
        await connection.ExecuteAsync(sql, model);
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? model = null)
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();
        return await connection.QueryAsync<T>(sql, model);
    }

    public async Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object model)
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();
        return await connection.QueryFirstOrDefaultAsync<T>(sql, model);
    }
}
