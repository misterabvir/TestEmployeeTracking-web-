namespace Core.Users.Commands.Register;

/// <summary>
/// Request with registration data
/// </summary>
/// <param name="Email"> Email of user </param>
/// <param name="Password"> Password of user </param>
public record RegisterCommandRequest(string Email, string Password);
