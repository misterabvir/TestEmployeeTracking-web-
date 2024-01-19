using FluentValidation;

namespace ApplicationCore.Departments.Commands.SetParent;

/// <summary>
/// Validator for <see cref="SetDepartmentParentCommand"/>
/// </summary>
public class SetDepartmentParentCommandValidator : AbstractValidator<SetDepartmentParentCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SetDepartmentParentCommandValidator"/> class.
    /// </summary>
    public SetDepartmentParentCommandValidator()
    {
        // Create rule for department id : not null, not empty
        RuleFor(x => x.Request.DepartmentId).NotNull().NotEmpty();
        
        // Create rule for parent department id : not empty when not null, not equal to department id when not null
        RuleFor(x => x.Request.ParentDepartmentId)
            .NotEqual(Guid.Empty).When(x => x != null)
            .NotEqual(x => x.Request.DepartmentId).When(x => x != null);
    }
}
