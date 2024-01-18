using FluentValidation;

namespace ApplicationCore.Employees.Commands.Create;

/// <summary>
/// Validator for <see cref="CreateEmployeeCommand"/>
/// </summary>
public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    /// <summary>
    /// Minimum length of last name
    /// </summary>
    private const int LastNameMinimumLength = 2;
    /// <summary>
    /// Minimum length of first name
    /// </summary>
    private const int FirstNameMinimumLength = 2;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateEmployeeCommandValidator"/> class.
    /// </summary>
    public CreateEmployeeCommandValidator() {
        
        // Create rule for last name : not null, not empty, minimum length
        RuleFor(e => e.Request.FirstName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(FirstNameMinimumLength);
        
        // Create rule for first name : not null, not empty, minimum length
        RuleFor(e => e.Request.LastName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(LastNameMinimumLength);
        
        // Create rule for department id : not empty
        RuleFor(e => e.Request.DepartmentId)
            .NotEmpty();
    }
}
