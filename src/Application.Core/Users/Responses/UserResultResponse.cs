using Core.Abstractions.Services;
using Entities.Users;

namespace Core.Users.Responses;

/// <summary>
/// Response with user data
/// </summary>
public class UserResultResponse
{
    /// <summary>
    /// Id of user
    /// </summary>
    public Guid Id { get; private set; }
    /// <summary>
    /// Email of user
    /// </summary>
    public string Email { get; private set; } = null!;
    /// <summary>
    /// Token for user login
    /// </summary>
    public string Token { get; private set; } = null!;

    /// <summary>
    /// Creates new instance of <see cref="UserResultResponse"/> from domain entity
    /// </summary>
    /// <param name="user"> Domain entity of user to convert </param>
    /// <param name="tokenGenerator"> Generator for token </param>
    /// <returns> Instance of <see cref="UserResultResponse"/> </returns>
    internal static UserResultResponse FromDomain(User user, ITokenGenerator tokenGenerator)
    {
        return new()
        {
            Id = user.Id.Value,
            Email = user.Email.Value,
            Token = tokenGenerator.Generate(user.Email.Value)
        };
    }
}
