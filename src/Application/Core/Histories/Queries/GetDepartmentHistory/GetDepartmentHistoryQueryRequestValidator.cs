using FluentValidation;

namespace ApplicationCore.Histories.Queries.GetDepartmentHistory;

public class GetDepartmentHistoryQueryRequestValidator : AbstractValidator<GetDepartmentHistoryQueryRequest> 
{
    public GetDepartmentHistoryQueryRequestValidator()
    {
        RuleFor(x => x.DepartmentId).NotNull().NotEmpty();
    }
}
