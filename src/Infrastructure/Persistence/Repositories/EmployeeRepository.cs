using ApplicationCore.Abstractions.Repositories;
using Dapper;
using Entities.Abstractions;
using Entities.Departments.ValueObjects;
using Entities.Employees;
using Persistence.Common;
using Persistence.DTO;

namespace Persistence.Repositories;

internal sealed class EmployeeRepository : IEmployeeRepository
{
    private readonly DbService _service;

    public EmployeeRepository(DbService service)
    {
        _service = service;
    }

    public async Task Create(Employee employee, CancellationToken cancellationToken)
    {
        string sql = """
                    INSERT INTO Employees(Id, LastName, FirstName, DepartmentId) 
                    VALUES(@id, @lastname, @firstname, @departmentId);
                    """;
        CommandDefinition command = new(
            commandText: sql,
            parameters: EmployeeDto.FromDomain(employee),
            cancellationToken: cancellationToken);
        await _service.ExecuteAsync(command);
    }

    public async Task Delete(Id Id, CancellationToken cancellationToken)
    {
        string sql = """
                DELETE 
                FROM Employees 
                WHERE Id=@Id
                """;
        CommandDefinition command = new(
            commandText: sql,
            cancellationToken: cancellationToken);
        await _service.ExecuteAsync(command);
    }

    public async Task<IEnumerable<Employee>> Get(CancellationToken cancellationToken)
    {
        string sql = """
                    SELECT Id, LastName, FirstName, DepartmentId 
                    FROM Employees;
                    """;
        CommandDefinition command = new(
            commandText: sql,
            cancellationToken: cancellationToken);
        var result = await _service.QueryAsync<EmployeeDto>(command);
        return result.Select(s => s.ToDomain());
    }

    public async Task<Employee?> Get(Id id, CancellationToken cancellationToken)
    {
        string sql = """
                    SELECT Id, LastName, FirstName, DepartmentId 
                    FROM Employees 
                    WHERE Id=@Id;
                    """;
        CommandDefinition command = new(
            commandText: sql,
            parameters: new { Id = id.Value },
            cancellationToken: cancellationToken);
        var result = await _service.QueryFirstOrDefaultAsync<EmployeeDto>(command);
        return result?.ToDomain();
    }

    public async Task<IEnumerable<Employee>> GetByDepartmentId(DepartmentId departmentId, CancellationToken cancellationToken)
    {
        string sql = """
                    SELECT Id, LastName, FirstName, DepartmentId 
                    FROM Employees 
                    WHERE DepartmentId=@DepartmentId;
                    """;
        CommandDefinition command = new(
            commandText: sql,
            parameters: new { DepartmentId = departmentId.Value },
            cancellationToken: cancellationToken);
        var result = await _service.QueryAsync<EmployeeDto>(command);
        return result.Select(x => x.ToDomain());
    }

    public async Task Update(Employee entity, CancellationToken cancellationToken)
    {
        string sql = """
                    UPDATE  Employees 
                    SET LastName=@LastNam, FirstName=@FirstName, DepartmentId=@DepartmentId
                    WHERE Id=@Id;
                    """;
        CommandDefinition command = new(
            commandText: sql,
            parameters: EmployeeDto.FromDomain(entity),
            cancellationToken: cancellationToken);
        await _service.ExecuteAsync(command);
    }
}
