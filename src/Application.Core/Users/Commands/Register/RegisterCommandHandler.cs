using ApplicationCore.Abstractions.Common;
using ApplicationCore.Abstractions.Repositories;
using Core.Abstractions.Services;
using Core.Users.Responses;
using Domain.Common;
using Entities.Users;
using Entities.Users.ValueObjects;
using static Core.Errors;

namespace Core.Users.Commands.Register;

public class RegisterCommandHandler : ICommandHandler<RegisterCommand, Result<UserResultResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IEncryption _encryption;
    private readonly ITokenGenerator _tokenGenerator;

    public RegisterCommandHandler(IEncryption encryption, IUserRepository userRepository, ITokenGenerator tokenGenerator)
    {
        _encryption = encryption;
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<Result<UserResultResponse>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        Email email = Email.Create(command.Request.Email);
        var user = await _userRepository.GetByEmail(email, cancellationToken);
        if(user is not null)
        {
            return new UserAlreadyExistError(email.Value);
        }

        Salt salt = Salt.CreateUnique();
        Password password = Password.Create(_encryption.EncryptPassword(command.Request.Password, salt.Value.ToString()));
        user = User.Create(email, password, salt);

        await _userRepository.Create(user, cancellationToken);

        return Result<UserResultResponse>.Success(UserResultResponse.FromDomain(user, _tokenGenerator));
    
    }
}