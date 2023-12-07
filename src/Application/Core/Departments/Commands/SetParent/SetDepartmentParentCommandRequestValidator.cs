using FluentValidation;

namespace Core.Departments.Commands.SetParent;

public class SetDepartmentParentCommandRequestValidator : AbstractValidator<SetDepartmentParentCommandRequest>
{
    public SetDepartmentParentCommandRequestValidator()
    {
        RuleFor(x => x.DepartmentId).NotNull().NotEmpty();
        RuleFor(x => x.ParentDepartmentId)
            .NotEmpty().When(x => x != null)
            .NotEqual(x => x.DepartmentId).When(x => x != null);
    }
}
