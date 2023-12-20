using FluentValidation;

namespace ApplicationCore.Histories.Queries.GetDepartmentHistory;

public class GetDepartmentHistoryQueryValidator : AbstractValidator<GetDepartmentHistoryQueryRequest> 
{
    public GetDepartmentHistoryQueryValidator()
    {
        RuleFor(x => x.DepartmentId).NotNull().NotEmpty();
    }
}
