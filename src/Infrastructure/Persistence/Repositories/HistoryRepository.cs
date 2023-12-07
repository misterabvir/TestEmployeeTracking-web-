using Core.Abstractions.Repositories;
using Entities.Abstractions;
using Entities.Departments.ValueObjects;
using Entities.Employees.ValueObjects;
using Entities.Histories;
using Microsoft.VisualBasic;
using Persistence.Common;
using Persistence.DTO;

namespace Persistence.Repositories;

internal class HistoryRepository : IHistoryRepository
{
    private readonly DbService _service;

    public HistoryRepository(DbService service)
    {
        _service = service;
    }

    public async Task Create(History history, CancellationToken cancellationToken)
    {
        string sql = @"INSERT INTO History(Id, EmployeeId, DepartmentId, StartDate, EndDate)
                       VALUE(@Id, @EmployeeId, @DepartmentId, @StartDate, @EndDate)";
        await _service.ExecuteAsync(sql, HistoryDto.FromDomain(history));
    }

    public async Task Delete(Id Id, CancellationToken cancellationToken)
    {
        string sql = @"DELETE FROM History WHERE Id=@Id";
        await _service.ExecuteAsync(sql, new { Id = Id.Value });
    }

    public async Task<IEnumerable<History>> Get(CancellationToken cancellationToken)
    {
        string sql = @"SELECT Id, EmployeeId, DepartmentId, StartDate, EndDate FROM History";
        IEnumerable<HistoryDto> result = await _service.QueryAsync<HistoryDto>(sql);
        return result.Select(h => h.ToDomain());
    }

    public async Task<History?> Get(Id Id, CancellationToken cancellationToken)
    {
        string sql = @"SELECT Id, EmployeeId, DepartmentId, StartDate, EndDate FROM History WHERE Id=@Id";
        var result = await _service.QueryFirstOrDefaultAsync<HistoryDto>(sql, new { Id = Id.Value });
        return result?.ToDomain();
    }

    public async Task<History?> Get(EmployeeId employeeId, DepartmentId departmentId, CancellationToken cancellationToken)
    {
        string sql = """
            SELECT Id, EmployeeId, DepartmentId, StartDate, EndDate 
            FROM History 
            WHERE 
                EmployeeId=@EmployeeId AND
                DepartmentId=@DepartmentId AND
                EndDate IS NULL;
            """;
        var result = await _service.QueryFirstOrDefaultAsync<HistoryDto>(sql, 
            new { EmployeeId = employeeId.Value, DepartmentId = departmentId.Value });
        return result?.ToDomain();
    }

    public async Task Update(History entity, CancellationToken cancellationToken)
    {
        string sql = @"UPDATE History SET EndDate=@EndDate WHERE Id=@Id";
        await _service.ExecuteAsync(sql, HistoryDto.FromDomain(entity));
    }
}
