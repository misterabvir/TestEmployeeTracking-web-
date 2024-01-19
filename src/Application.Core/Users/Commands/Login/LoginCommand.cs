using ApplicationCore.Abstractions.Common;
using Core.Users.Responses;
using Domain.Common;

namespace Core.Users.Commands.Login;

/// <summary>
/// Command to login
/// </summary>
/// <param name="Request"> Request with login data </param>
public record LoginCommand(LoginCommandRequest Request) : ICommand<Result<UserResultResponse>>;
