using FluentValidation;

namespace ApplicationCore.Departments.Commands.Create;

public class CreateDepartmentCommandRequestValidator : AbstractValidator<CreateDepartmentCommandRequest>
{
    public const int MinimumTitleLength = 5;

    public CreateDepartmentCommandRequestValidator()
    {
        RuleFor(x => x.Title).NotNull().NotEmpty().MinimumLength(MinimumTitleLength);
        RuleFor(x => x.ParentDepartmentId).NotEqual(Guid.Empty).When(x => x is not null);
    }
}
