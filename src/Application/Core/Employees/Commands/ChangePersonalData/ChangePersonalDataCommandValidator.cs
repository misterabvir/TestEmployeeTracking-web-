using FluentValidation;

namespace ApplicationCore.Employees.Commands.ChangePersonalData;

/// <summary>
/// Validator for <see cref="ChangePersonalDataCommand"/>
/// </summary>
public class ChangePersonalDataCommandValidator : AbstractValidator<ChangePersonalDataCommand>
{
   /// <summary>
   /// Minimum length of last name
   /// </summary>
    private const int LastNameMinimumLength = 2;
    /// <summary>
    ///  Minimum length of first name
    /// </summary>
    private const int FirstNameMinimumLength = 2;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ChangePersonalDataCommandValidator"/> class.
    /// </summary>
    public ChangePersonalDataCommandValidator()
    {
        // Create rule for employee id : not null, not empty
        RuleFor(e => e.Request.EmployeeId).NotNull().NotEmpty();
        // Create rule for last name : not null, not empty, minimum length
        RuleFor(e => e.Request.LastName).NotNull().NotEmpty().MinimumLength(LastNameMinimumLength);
        // Create rule for first name : not null, not empty, minimum length
        RuleFor(e => e.Request.FirstName).NotNull().NotEmpty().MinimumLength(FirstNameMinimumLength);
    }
}
