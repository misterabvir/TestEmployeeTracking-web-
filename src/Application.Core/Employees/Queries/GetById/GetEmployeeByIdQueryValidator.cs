using FluentValidation;

namespace ApplicationCore.Employees.Queries.GetById;

/// <summary>
/// Validator for <see cref="GetEmployeeByIdQuery"/>
/// </summary>
public class GetEmployeeByIdQueryValidator : AbstractValidator<GetEmployeeByIdQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetEmployeeByIdQueryValidator"/> class.
    /// </summary>
    public GetEmployeeByIdQueryValidator()
    { 
        // Create rule for employee id : not null, not empty
        RuleFor(e=>e.Request.EmployeeId)
            .NotNull()
            .NotEmpty();
    }
}