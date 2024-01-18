using FluentValidation;

namespace ApplicationCore.Departments.Commands.ChangeTitle;

/// <summary>
/// Validator for <see cref="ChangeDepartmentTitleCommand"/>.
/// </summary>
public class ChangeDepartmentTitleCommandValidator : AbstractValidator<ChangeDepartmentTitleCommand>
{
    /// <summary>
    /// Minimum length of title
    /// </summary>
    private const int MinimumLengthTitle = 3;

    /// <summary>
    /// Initializes a new instance of the ChangeDepartmentTitleCommandValidator class.
    /// </summary>
    public ChangeDepartmentTitleCommandValidator()
    {
        // Create rule for title : not null, not empty, minimum length
        RuleFor(x => x.Request.Title)
            .NotNull()
            .NotEmpty()
            .MinimumLength(MinimumLengthTitle);
    }
}