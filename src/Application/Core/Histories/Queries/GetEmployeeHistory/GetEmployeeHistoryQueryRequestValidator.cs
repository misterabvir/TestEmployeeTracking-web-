using FluentValidation;

namespace ApplicationCore.Histories.Queries.GetEmployeeHistory;

public class GetEmployeeHistoryQueryRequestValidator : AbstractValidator<GetEmployeeHistoryQueryRequest>
{
    public GetEmployeeHistoryQueryRequestValidator()
    {
        RuleFor(x => x.EmployeeId).NotNull().NotEmpty();
    }
}
