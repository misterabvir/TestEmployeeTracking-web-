using ApplicationCore.Abstractions.Repositories;
using Dapper;
using Entities.Abstractions;
using Entities.Abstractions.Shared;
using Entities.Departments.ValueObjects;
using Entities.Employees.ValueObjects;
using Entities.Histories;
using Persistence.Common;

namespace Persistence.Repositories;

internal sealed class HistoryRepository : IHistoryRepository
{
    private readonly DbService _service;

    public HistoryRepository(DbService service)
    {
        _service = service;
    }

    public async Task Create(History history, CancellationToken cancellationToken)
    {
        string sql = """
                INSERT INTO History(Id, EmployeeId, DepartmentId, StartDate, EndDate)
                VALUES(@Id, @EmployeeId, @DepartmentId, @StartDate, @EndDate)
                """;
        CommandDefinition command = new(
            commandText: sql,
            parameters: history,
            cancellationToken: cancellationToken);
        await _service.ExecuteAsync(command);
    }

    public async Task Delete(Id Id, CancellationToken cancellationToken)
    {
        string sql = """
                    DELETE 
                    FROM History 
                    WHERE Id=@Id
                    """;
        CommandDefinition command = new(
            commandText: sql,
            parameters: new { Id = Id.Value },
            cancellationToken: cancellationToken);
        await _service.ExecuteAsync(command);
    }

    public async Task<IEnumerable<History>> Get(CancellationToken cancellationToken)
    {
        string sql = """
            SELECT Id, EmployeeId, DepartmentId, StartDate, EndDate 
            FROM History
            """;
        CommandDefinition command = new(
            commandText: sql,
            cancellationToken: cancellationToken);
        return await _service.QueryAsync<History>(command);
    }

    public async Task<History?> Get(Id Id, CancellationToken cancellationToken)
    {
        string sql = """
            SELECT Id, EmployeeId, DepartmentId, StartDate, EndDate 
            FROM History 
            WHERE Id=@Id
            """;
        CommandDefinition command = new(
            commandText: sql,
            parameters: new { Id = Id.Value },
            cancellationToken: cancellationToken);
        return await _service.QueryFirstOrDefaultAsync<History>(command);
    }

    public async Task<History?> Get(EmployeeId employeeId, DepartmentId departmentId, CancellationToken cancellationToken)
    {
        string sql = """
                SELECT Id, EmployeeId, DepartmentId, StartDate, EndDate 
                FROM History 
                WHERE EmployeeId=@EmployeeId AND DepartmentId=@DepartmentId AND EndDate IS NULL;
                """;
        CommandDefinition command = new(
            commandText: sql,
            parameters: new { EmployeeId = employeeId.Value, DepartmentId = departmentId.Value },
            cancellationToken: cancellationToken);
        return await _service.QueryFirstOrDefaultAsync<History>(command);
    }

    public async Task<IEnumerable<History>> GetDepartmentHistory(DepartmentId departmentId, CancellationToken cancellationToken)
    {
        string sql = """
            SELECT Id, EmployeeId, DepartmentId, StartDate, EndDate 
            FROM History 
            WHERE DepartmentId=@DepartmentId
            """;
        CommandDefinition command = new(
            commandText: sql,
            parameters: new { DepartmentId = departmentId.Value },
            cancellationToken: cancellationToken);
        return await _service.QueryAsync<History>(command);
    }

    public async Task<IEnumerable<History>> GetEmployeeHistory(EmployeeId employeeId, CancellationToken cancellationToken)
    {
        string sql = """
            SELECT Id, EmployeeId, DepartmentId, StartDate, EndDate 
            FROM History 
            WHERE EmployeeId=@EmployeeId
            """;
        CommandDefinition command = new(
            commandText: sql,
            parameters: new { EmployeeId = employeeId.Value },
            cancellationToken: cancellationToken);
        return await _service.QueryAsync<History>(command);
    }

    public async Task Update(History entity, CancellationToken cancellationToken)
    {
        string sql = """
            UPDATE History 
            SET EndDate=@EndDate 
            WHERE Id=@Id
            """;
        CommandDefinition command = new(
        commandText: sql,
        parameters: entity,
        cancellationToken: cancellationToken);
        await _service.ExecuteAsync(command);
    }
}
