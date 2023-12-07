using FluentValidation;

namespace Core.Departments.Commands.Create;

public class CreateDepartmentCommandRequestValidator : AbstractValidator<CreateDepartmentCommandRequest>
{
    public const int MinimumTitleLength = 5;

    public CreateDepartmentCommandRequestValidator()
    {
        RuleFor(x => x.Title).NotNull().NotEmpty().MinimumLength(MinimumTitleLength);
        RuleFor(x => x.ParentDepartmentId).NotEmpty().When(x => x is not null);
    }
}
