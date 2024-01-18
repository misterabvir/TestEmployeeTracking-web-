using FluentValidation;

namespace ApplicationCore.Employees.Commands.ChangeDepartment;

/// <summary>
/// Validator for <see cref="ChangeDepartmentCommand"/>
/// </summary>
public class ChangeDepartmentCommandValidator : AbstractValidator<ChangeDepartmentCommand>
{
   /// <summary>
   /// Initializes a new instance of the <see cref="ChangeDepartmentCommandValidator"/> class.
   /// </summary>
    public ChangeDepartmentCommandValidator()
    {
        // Create rule for employee id : not empty
        RuleFor(x => x.Request.EmployeeId).NotEmpty();
        // Create rule for department id : not empty
        RuleFor(x => x.Request.DepartmentId).NotEmpty();
    }
}
