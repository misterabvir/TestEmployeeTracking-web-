using FluentValidation;

namespace ApplicationCore.Departments.Queries.GetById;

public class GetDepartmentByIdQueryRequestValidator : AbstractValidator<GetDepartmentByIdQueryRequest>
{
    public GetDepartmentByIdQueryRequestValidator()
    {
        RuleFor(x => x.DepartmentId).NotEmpty();
    }
}
