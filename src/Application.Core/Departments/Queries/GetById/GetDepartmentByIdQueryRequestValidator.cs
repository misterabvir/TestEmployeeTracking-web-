using FluentValidation;

namespace ApplicationCore.Departments.Queries.GetById;

/// <summary>
/// Validator for <see cref="GetDepartmentByIdQueryRequest"/>
/// </summary>
public class GetDepartmentByIdQueryRequestValidator : AbstractValidator<GetDepartmentByIdQueryRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDepartmentByIdQueryRequestValidator"/> class.
    /// </summary>
    public GetDepartmentByIdQueryRequestValidator()
    {
        // Create rule for department id : not null, not empty
        RuleFor(x => x.DepartmentId)
            .NotNull()
            .NotEmpty();
    }
}
