using Dapper;
using Microsoft.Data.SqlClient;
using Persistence.Common.Handlers;

namespace Persistence.Common;

internal class DbService
{
    private readonly string _connectionString;
    public DbService(string connectionString)
    {
        _connectionString = connectionString;
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        SqlMapper.AddTypeHandler(new TitleTypeHandler());
        SqlMapper.AddTypeHandler(new FirstNameTypeHandler());
        SqlMapper.AddTypeHandler(new LastNameTypeHandler());
        SqlMapper.AddTypeHandler(new EmployeeIdTypeHandler());
        SqlMapper.AddTypeHandler(new DepartmentIdTypeHandler());
        SqlMapper.AddTypeHandler(new HistoryIdTypeHandler());
        SqlMapper.AddTypeHandler(new EmailHandler());
        SqlMapper.AddTypeHandler(new PasswordHandler());
        SqlMapper.AddTypeHandler(new SaltHandler());       
        SqlMapper.AddTypeHandler(new UserIdHandler());       
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
