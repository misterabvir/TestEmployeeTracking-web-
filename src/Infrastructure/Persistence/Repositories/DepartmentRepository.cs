using ApplicationCore.Abstractions.Repositories;
using Dapper;
using Entities.Abstractions;
using Entities.Departments;
using Entities.Departments.ValueObjects;
using Persistence.Common;
using Persistence.DTO;

namespace Persistence.Repositories;

internal sealed class DepartmentRepository : IDepartmentRepository
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
        CommandDefinition command = new(
            commandText: sql,
            parameters: DepartmentDto.FromDomain(entity),
            cancellationToken: cancellationToken);
        await _service.ExecuteAsync(command);
    }

    public async Task Delete(Id Id, CancellationToken cancellationToken)
    {
        string sql = """
                DELETE 
                FROM Departments 
                WHERE Id=@Id;
                """;
        CommandDefinition command = new(
            commandText: sql,
            parameters: new { Id = Id.Value },
            cancellationToken: cancellationToken);
        await _service.ExecuteAsync(command);
    }

    public async Task<Department?> Get(Id Id, CancellationToken cancellationToken)
    {
        string sql = """
                SELECT Id, ParentId, Title 
                FROM Departments 
                WHERE Id=@Id;
                """;
        CommandDefinition command = new(
            commandText: sql,
            parameters: new { Id = Id.Value },
            cancellationToken: cancellationToken);
        var result = await _service.QueryFirstOrDefaultAsync<DepartmentDto>(command);
        return result?.ToDomain();
    }

    public async Task<IEnumerable<Department>> Get(CancellationToken cancellationToken)
    {
        string sql = """
                SELECT Id, ParentId, Title 
                FROM Departments;
                """;
        CommandDefinition command = new(
            commandText: sql,
            cancellationToken: cancellationToken);
        var result = await _service.QueryAsync<DepartmentDto>(command);
        return result.Select(x => x.ToDomain());
    }

    public async Task<Department?> GetByNameAndParentId(Title title, DepartmentId? parentId, CancellationToken cancellationToken)
    {
        string sql = parentId is not null ? """
                SELECT Id, ParentId, Title 
                FROM Departments 
                WHERE Title=@Title AND ParentId = @ParentId;
                """ :
                """
                SELECT Id, ParentId, Title 
                FROM Departments 
                WHERE Title=@Title AND ParentId IS NULL;
                """;

        CommandDefinition command = new(
            commandText: sql,
            parameters: new { Title = title.Value, ParentId = parentId?.Value ?? Guid.Empty },
            cancellationToken: cancellationToken);
        var result = await _service.QueryFirstOrDefaultAsync<DepartmentDto>(command);
        return result?.ToDomain();
    }

    public async Task Update(Department entity, CancellationToken cancellationToken)
    {
        string sql = """
                UPDATE Departments
                SET ParentId = @ParentId, Title = @Title
                WHERE Id=@Id;
                """;
        CommandDefinition command = new(
            commandText: sql,
            parameters: DepartmentDto.FromDomain(entity),
            cancellationToken: cancellationToken);
        await _service.ExecuteAsync(command);
    }
}
