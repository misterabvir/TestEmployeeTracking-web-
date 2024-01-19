using ApplicationCore.Abstractions.Repositories;
using Dapper;
using Entities.Abstractions;
using Entities.Abstractions.Shared;
using Entities.Users;
using Entities.Users.ValueObjects;
using Persistence.Common;
using System.Threading;

namespace Persistence.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly DbService _service;

    public UserRepository(DbService service)
    {
        _service = service;
    }


    public async Task Create(User entity, CancellationToken cancellationToken)
    {
        string sql = """
                INSERT INTO Users(Id, Email, Password, Salt)
                VALUES(@Id, @Email, @Password, @Salt)
                """;
        CommandDefinition command = new(
            commandText: sql,
            parameters: entity,
            cancellationToken: cancellationToken);
        await _service.ExecuteAsync(command);
    }

    public async Task Delete(Id Id, CancellationToken cancellationToken)
    {
        string sql = """
                    DELETE 
                    FROM Users 
                    WHERE Id=@Id
                    """;
        CommandDefinition command = new(
            commandText: sql,
            parameters: new { Id = Id.Value },
            cancellationToken: cancellationToken);
        await _service.ExecuteAsync(command);
    }

    public async Task<IEnumerable<User>> Get(CancellationToken cancellationToken)
    {
        string sql = """
            SELECT Id, Email, Password, Salt
            FROM Users
            """;
        CommandDefinition command = new(
            commandText: sql,
            cancellationToken: cancellationToken);
        return await _service.QueryAsync<User>(command);
    }

    public async Task<User?> Get(Id Id, CancellationToken cancellationToken)
    {
        string sql = """
            SELECT Id, Email, Password, Salt
            FROM Users 
            WHERE Id=@Id
            """;
        CommandDefinition command = new(
            commandText: sql,
            parameters: new { Id = Id.Value },
            cancellationToken: cancellationToken);
        return await _service.QueryFirstOrDefaultAsync<User>(command);
    }

    public async Task<User?> GetByEmail(Email email, CancellationToken cancellationToken)
    {
        string sql = """
            SELECT Id, Email, Password, Salt
            FROM Users 
            WHERE Email=@Email
            """;
        CommandDefinition command = new(
            commandText: sql,
            parameters: new { Email = email.Value },
            cancellationToken: cancellationToken);
        return await _service.QueryFirstOrDefaultAsync<User>(command);
    }

    public async Task Update(User entity, CancellationToken cancellationToken)
    {
        string sql = """
            UPDATE Users 
            SET 
                Email=@Email,
                Password=@Password,
                Salt=@Salt
            WHERE Id=@Id
            """;
        CommandDefinition command = new(
        commandText: sql,
        parameters: entity,
        cancellationToken: cancellationToken);
        await _service.ExecuteAsync(command);
    }
}
