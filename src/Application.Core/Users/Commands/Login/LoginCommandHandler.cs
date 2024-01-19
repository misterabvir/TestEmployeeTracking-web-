using ApplicationCore.Abstractions.Common;
using ApplicationCore.Abstractions.Repositories;
using Core.Abstractions.Services;
using Core.Users.Responses;
using Domain.Common;
using Entities.Users.ValueObjects;
using static Core.Errors;

namespace Core.Users.Commands.Login;

/// <summary>
/// Handler for user login
/// </summary>
public class LoginCommandHandler : ICommandHandler<LoginCommand, Result<UserResultResponse>>
{
    /// <summary>
    /// Repository for <see cref="User"/>
    /// </summary>
    private readonly IUserRepository _userRepository;
    /// <summary>
    /// Service for encrypt password
    /// </summary>
    private readonly IEncryption _encryption;
    /// <summary>
    /// Service to generate token
    /// </summary>
    private readonly ITokenGenerator _tokenGenerator;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginCommandHandler"/> class.
    /// </summary>
    /// <param name="encryption"> Service for encrypt password </param>
    /// <param name="userRepository"> Repository for <see cref="User"/></param>
    /// <param name="tokenGenerator"> Service to generate token </param>
    public LoginCommandHandler(IEncryption encryption, IUserRepository userRepository, ITokenGenerator tokenGenerator)
    {
        _encryption = encryption;
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
    }

    /// <summary>
    /// Handler for user login
    /// </summary>
    /// <param name="command"> Command to login </param>
    /// <param name="cancellationToken"> Cancellation token </param>
    /// <returns> Result with user data or error </returns>
    public async Task<Result<UserResultResponse>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        // Check if user exists
        Email email = Email.Create(command.Request.Email);
        var user = await _userRepository.GetByEmail(email, cancellationToken);
        if(user is null)
        {
            return new UserFailedLoginError(email.Value);
        }

        // Check if password is correct
        Password password = Password.Create(_encryption.EncryptPassword(command.Request.Password, user.Salt.Value.ToString()));
        if (password != user.Password)
        {
            return new UserFailedLoginError(email.Value);
        }

        // Return user
        return Result<UserResultResponse>.Success(UserResultResponse.FromDomain(user, _tokenGenerator));
    }
}