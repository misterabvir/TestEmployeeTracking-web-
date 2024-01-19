using FluentValidation;

namespace ApplicationCore.Histories.Queries.GetDepartmentHistory;

/// <summary>
/// Validator for <see cref="GetDepartmentHistoryQuery"/>
/// </summary>
public class GetDepartmentHistoryQueryValidator : AbstractValidator<GetDepartmentHistoryQueryRequest> 
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDepartmentHistoryQueryValidator"/> class.
    /// </summary>
    public GetDepartmentHistoryQueryValidator()
    {
        // Create rule for department id : not null, not empty
        RuleFor(x => x.DepartmentId)
            .NotNull()
            .NotEmpty();
    }
}
