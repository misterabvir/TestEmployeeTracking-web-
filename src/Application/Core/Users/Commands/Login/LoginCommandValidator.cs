using FluentValidation;

namespace Core.Users.Commands.Login;

/// <summary>
/// Validator for <see cref="LoginCommand"/>
/// </summary>
public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LoginCommandValidator"/> class.
    /// </summary>
    public LoginCommandValidator()
    {
        // Create rule for email and password : not null, not empty, must be valid email
        RuleFor(x => x.Request.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();
        // Create rule for password : not null, not empty, must be valid password
        RuleFor(x => x.Request.Password)
            .NotNull()
            .NotEmpty()
            .Matches(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
    }
}
