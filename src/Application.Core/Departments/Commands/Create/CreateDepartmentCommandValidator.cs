using FluentValidation;

namespace ApplicationCore.Departments.Commands.Create;

/// <summary>
/// Validator for <see cref="CreateDepartmentCommand"/>
/// </summary>
public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{
    /// <summary>
    /// Minimum title length
    /// </summary>
    public const int MinimumTitleLength = 5;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateDepartmentCommandValidator"/> class.
    /// </summary>
    public CreateDepartmentCommandValidator()
    {
        // Create rule for title : not null, not empty, minimum length
        RuleFor(x => x.Request.Title)
            .NotNull()
            .NotEmpty()
            .MinimumLength(MinimumTitleLength);
        // Create rule for parent department id : not empty when not null
        RuleFor(x => x.Request.ParentDepartmentId)
            .NotEqual(Guid.Empty)
            .When(x => x is not null);
    }
}
