namespace Core.Users.Commands.Login;

/// <summary>
/// Request with login data
/// </summary>
/// <param name="Email"> Email of user </param>
/// <param name="Password"> Password of user </param>
public record LoginCommandRequest(string Email, string Password);
