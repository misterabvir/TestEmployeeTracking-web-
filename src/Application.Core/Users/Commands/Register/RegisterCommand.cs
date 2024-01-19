using ApplicationCore.Abstractions.Common;
using Core.Users.Responses;
using Domain.Common;

namespace Core.Users.Commands.Register;

/// <summary>
/// Command to register new user
/// </summary>
/// <param name="Request"> Request with registration data </param>
public record RegisterCommand(RegisterCommandRequest Request) : ICommand<Result<UserResultResponse>>;
