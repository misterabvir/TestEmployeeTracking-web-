using FluentValidation;

namespace ApplicationCore.Employees.Commands.ChangeDepartment;

public class ChangeDepartmentCommandRequestValidator : AbstractValidator<ChangeDepartmentCommandRequest>
{
    public ChangeDepartmentCommandRequestValidator()
    {
        RuleFor(x => x.EmployeeId).NotEmpty();
        RuleFor(x => x.DepartmentId).NotEmpty();
    }
}
