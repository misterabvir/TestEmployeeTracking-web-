using Entities.Departments;
using FluentValidation;

namespace Application.Core.Employees.Queries.GetByDepartmentId;

/// <summary>
/// Validator for query get employees in <see cref="Department"/>
/// </summary>
public class GetEmployeesByDepartmentIdQueryValidator : AbstractValidator<GetEmployeesByDepartmentIdQuery>
{
    public GetEmployeesByDepartmentIdQueryValidator()
    {
        // Create rule for <see cref="DepartmentId"/>: not null, not empty
        RuleFor(d => d.Request).NotNull().NotEmpty();
    }
}
