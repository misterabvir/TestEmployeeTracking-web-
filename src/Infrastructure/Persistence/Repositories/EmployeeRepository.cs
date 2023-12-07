using Core.Abstractions.Repositories;
using Entities.Abstractions;
using Entities.Employees;
using Persistence.Common;
using Persistence.DTO;

namespace Persistence.Repositories;

internal class EmployeeRepository : IEmployeeRepository
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
        await _service.ExecuteAsync(sql, EmployeeDto.FromDomain(employee));
    }

    public async Task Delete(Id Id, CancellationToken cancellationToken)
    {
        string sql = "DELETE FROM Employees WHERE Id=@Id";
        await _service.ExecuteAsync(sql, new { Id = Id.Value });
    }

    public async Task<IEnumerable<Employee>> Get(CancellationToken cancellationToken)
    {
        string sql = """
                    SELECT 
                        Id, 
                        LastName, 
                        FirstName, 
                        DepartmentId 
                    FROM 
                        Employees;
                    """;
        IEnumerable<EmployeeDto> result = await _service.QueryAsync<EmployeeDto>(sql);
        return result.Select(s => s.ToDomain());
    }

    public async Task<Employee?> Get(Id id, CancellationToken cancellationToken)
    {
        string sql = """
                    SELECT 
                        Id, 
                        LastName, 
                        FirstName, 
                        DepartmentId 
                    FROM 
                        Employees 
                    WHERE 
                        Id=@Id;
                    """;
        EmployeeDto? result = await _service.QueryFirstOrDefaultAsync<EmployeeDto>(sql, new { Id = id.Value});        
        return result?.ToDomain();
    }

  

    public async Task Update(Employee entity, CancellationToken cancellationToken)
    {
        string sql = """
                    UPDATE 
                        Employees 
                    SET 
                        LastName=@LastName, 
                        FirstName=@FirstName, 
                        DepartmentId=@DepartmentId
                    WHERE 
                        Id=@Id;
                    """;
        await _service.ExecuteAsync(sql, EmployeeDto.FromDomain(entity));
    }
}
