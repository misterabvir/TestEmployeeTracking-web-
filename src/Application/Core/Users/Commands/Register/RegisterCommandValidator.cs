using FluentValidation;

namespace Core.Users.Commands.Register;

/// <summary>
/// Validator for <see cref="RegisterCommand"/>
/// </summary>
public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterCommandValidator"/> class.
    /// </summary>
    public RegisterCommandValidator()
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
