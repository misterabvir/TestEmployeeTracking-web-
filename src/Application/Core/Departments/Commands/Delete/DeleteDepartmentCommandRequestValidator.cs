using FluentValidation;

namespace ApplicationCore.Departments.Commands.Delete;

public class DeleteDepartmentCommandRequestValidator : AbstractValidator<DeleteDepartmentCommandRequest>
{
    public DeleteDepartmentCommandRequestValidator()
    {
        RuleFor(x => x.DepartmentId).NotNull().NotEmpty();
    }
}
