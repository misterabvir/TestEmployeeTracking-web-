using Core.Abstractions.Repositories;
using Entities.Abstractions;
using Entities.Departments;
using Persistence.Common;
using Persistence.DTO;

namespace Persistence.Repositories;

internal class DepartmentRepository : IDepartmentRepository
{
    private readonly DbService _service;

    public DepartmentRepository(DbService service)
    {
        _service = service;
    }

    public async Task Create(Department entity, CancellationToken cancellationToken)
    {
        string sql = """
                INSERT INTO Departments (Id, ParentId, Title)
                VALUES (@Id, @ParentId, @Title);
                """;
        await _service.ExecuteAsync(sql, DepartmentDto.FromDomain(entity));
    }

    public async Task Delete(Id Id, CancellationToken cancellationToken)
    {
        string sql = """
                DELETE FROM Departments WHERE Id=@Id;
                """;
        await _service.ExecuteAsync(sql, new {Id = Id.Value});
    }

    public async Task<Department?> Get(Id Id, CancellationToken cancellationToke)
    {
        string sql = """
                SELECT 
                    Id, 
                    ParentId, 
                    Title 
                FROM 
                    Departments WHERE Id=@Id;
                """;
        var result = await _service.QueryFirstOrDefaultAsync<DepartmentDto>(sql, new {Id = Id.Value});
        return result?.ToDomain();
    }

    public async Task<IEnumerable<Department>> Get(CancellationToken cancellationToken)
    {
        string sql = """
                SELECT 
                    Id, 
                    ParentId, 
                    Title 
                FROM 
                    Departments;
                """;
        IEnumerable<DepartmentDto> result = await _service.QueryAsync<DepartmentDto>(sql);
        return result.Select(x => x.ToDomain());
    }


    public async Task Update(Department entity, CancellationToken cancellationToken)
    {
        string sql = """
                UPDATE Departments
                SET 
                    ParentId = @ParentId,
                    Title = @Title
                WHERE Id=@Id;
                """;
        await _service.ExecuteAsync(sql, DepartmentDto.FromDomain(entity));
    }
}
