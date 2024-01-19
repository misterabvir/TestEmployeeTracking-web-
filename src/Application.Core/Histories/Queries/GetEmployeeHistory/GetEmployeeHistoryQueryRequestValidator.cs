using FluentValidation;

namespace ApplicationCore.Histories.Queries.GetEmployeeHistory;

/// <summary>
/// Validator for <see cref="GetEmployeeHistoryQueryRequest"/>
/// </summary>
public class GetEmployeeHistoryQueryRequestValidator : AbstractValidator<GetEmployeeHistoryQueryRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetEmployeeHistoryQueryRequestValidator"/> class.
    /// </summary>
    public GetEmployeeHistoryQueryRequestValidator()
    {
        // Create rule for employee id : not null, not empty
        RuleFor(x => x.EmployeeId)
            .NotNull()
            .NotEmpty();
    }
}
