using FluentValidation;

namespace ApplicationCore.Employees.Commands.Delete;

/// <summary>
/// Validator for <see cref="DeleteEmployeeCommand"/>
/// </summary>
public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteEmployeeCommandValidator"/> class.
    /// </summary>
    public DeleteEmployeeCommandValidator()
    {
        // Create rule for employee id : not null, not empty
        RuleFor(x => x.Request.EmployeeId)
            .NotNull()
            .NotEmpty();
    }
}
