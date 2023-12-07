using FluentValidation;

namespace Core.Employees.Commands.Delete;

public class DeleteEmployeeCommandRequestValidator : AbstractValidator<DeleteEmployeeCommandRequest>
{
    public DeleteEmployeeCommandRequestValidator()
    {
        RuleFor(x => x.EmployeeId).NotNull().NotEmpty();
    }
}
