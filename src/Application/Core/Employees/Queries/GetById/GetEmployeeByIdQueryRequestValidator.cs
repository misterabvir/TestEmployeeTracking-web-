using FluentValidation;

namespace ApplicationCore.Employees.Queries.GetById;

public class GetEmployeeByIdQueryRequestValidator : AbstractValidator<GetEmployeeByIdQueryRequest>
{
    public GetEmployeeByIdQueryRequestValidator()
    { 
        RuleFor(e=>e.EmployeeId).NotNull().NotEmpty();
    }
}