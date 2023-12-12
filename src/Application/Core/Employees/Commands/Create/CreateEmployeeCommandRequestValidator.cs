using FluentValidation;

namespace ApplicationCore.Employees.Commands.Create;

public class CreateEmployeeCommandRequestValidator : AbstractValidator<CreateEmployeeCommandRequest>
{
    private const int LastNameMinimumLength = 2;
    private const int FirstNameMinimumLength = 2;
    public CreateEmployeeCommandRequestValidator() {
        RuleFor(e => e.FirstName).NotNull().NotEmpty().MinimumLength(FirstNameMinimumLength);
        RuleFor(e => e.LastName).NotNull().NotEmpty().MinimumLength(LastNameMinimumLength);
        RuleFor(e => e.DepartmentId).NotEmpty();
    }
}
