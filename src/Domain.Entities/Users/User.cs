using Entities.Abstractions.General;
using Entities.Users.ValueObjects;

namespace Entities.Users;

/// <summary>
/// Domain entity of <see cref="User"/>
/// </summary>
public class User: Entity<UserId>
{
    /// <summary>
    /// Email of <see cref="User"/>
    /// </summary>
    public Email Email { get; private set; } = null!;
    /// <summary>
    /// Password of <see cref="User"/>
    /// </summary>
    public Password Password { get; private set; } = null!;
    /// <summary>
    /// Salt of <see cref="User"/> for encryption
    /// </summary>
    /// <value></value>
    public Salt Salt { get; private set; } = null!;

    private User() { }

    /// <summary>
    /// Create <see cref="User"/>
    /// </summary>
    /// <param name="email"> Email of <see cref="User"/></param>
    /// <param name="password"> Password of <see cref="User"/> </param>
    /// <param name="salt">Salt of <see cref="User"/> for encryption</param>
    /// <returns></returns>
    public static User Create(Email email, Password password, Salt salt)
    {
        return new()
        {
            Id = UserId.CreateUnique(),
            Email = email,
            Password = password,
            Salt = salt
        };
    }

    /// <summary>
    /// Create <see cref="User"/>
    /// </summary>
    /// <param name="id"> Id of <see cref="User"/></param>
    /// <param name="email"> Email of <see cref="User"/></param>
    /// <param name="password"> Password of <see cref="User"/> </param>
    /// <param name="salt">Salt of <see cref="User"/> for encryption</param>
    /// <returns></returns>
    public static User Create(UserId id, Email email, Password password, Salt salt)
    {
        return new()
        {
            Id = id,
            Email = email,
            Password = password,
            Salt = salt
        };
    }
}
