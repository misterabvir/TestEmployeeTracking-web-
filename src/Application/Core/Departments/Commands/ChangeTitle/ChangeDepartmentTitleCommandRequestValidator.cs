using FluentValidation;

namespace ApplicationCore.Departments.Commands.ChangeTitle;

public class ChangeDepartmentTitleCommandRequestValidator : AbstractValidator<ChangeDepartmentTitleCommandRequest>
{
    private const int MinimumLengthTitle = 3;

    public ChangeDepartmentTitleCommandRequestValidator()
    {
        RuleFor(x => x.Title).NotNull().NotEmpty().MinimumLength(MinimumLengthTitle);
    }
}
