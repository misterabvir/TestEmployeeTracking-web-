using FluentValidation;

namespace Core.Employees.Commands.ChangePersonalData;

public class ChangePersonalDataCommandRequestValidator : AbstractValidator<ChangePersonalDataCommandRequest>
{
    private const int LastNameMinimumLength = 2;
    private const int FirstNameMinimumLength = 2;
    public ChangePersonalDataCommandRequestValidator()
    {
        RuleFor(e => e.EmployeeId).NotNull().NotEmpty();
        RuleFor(e => e.LastName).NotNull().NotEmpty().MinimumLength(LastNameMinimumLength);
        RuleFor(e => e.FirstName).NotNull().NotEmpty().MinimumLength(FirstNameMinimumLength);
    }
}
