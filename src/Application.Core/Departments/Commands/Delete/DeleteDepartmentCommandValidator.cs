using FluentValidation;

namespace ApplicationCore.Departments.Commands.Delete;

/// <summary>
/// Validator for <see cref="DeleteDepartmentCommand"/>
/// </summary>
public class DeleteDepartmentCommandValidator : AbstractValidator<DeleteDepartmentCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteDepartmentCommandValidator"/> class.
    /// </summary>
    public DeleteDepartmentCommandValidator()
    {
        // Create rule for department id : not null, not empty
        RuleFor(x => x.Request.DepartmentId).NotNull().NotEmpty();
    }
}
