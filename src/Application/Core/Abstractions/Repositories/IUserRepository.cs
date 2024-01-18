using Entities.Users;
using Entities.Users.ValueObjects;

namespace ApplicationCore.Abstractions.Repositories;

/// <summary>
/// Repository for <see cref="User"/>
/// </summary>
public interface IUserRepository : IRepository<User>
{
    /// <summary>
    /// Get user by email
    /// </summary>
    /// <param name="email"> Email of user </param>
    /// <param name="cancellationToken"> CancellationToken </param>
    /// <returns> User or null </returns>
    Task<User?> GetByEmail(Email email, CancellationToken cancellationToken);
}