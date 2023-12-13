using Domain.Common;
using static Core.Errors;
using ApplicationCore.Employees.Responses;
using MediatR;

namespace ApplicationCore.Employees.Queries.GetById;

public sealed class GetEmployeeByIdQueryHandlerBehavior
    : IPipelineBehavior<GetEmployeeByIdQuery, Result<EmployeeResultResponse>>
{
    public async Task<Result<EmployeeResultResponse>> Handle(
        GetEmployeeByIdQuery request, 
        RequestHandlerDelegate<Result<EmployeeResultResponse>> next, 
        CancellationToken cancellationToken)
    {
        var validator = new GetEmployeeByIdQueryRequestValidator();
        var result = await validator.ValidateAsync(request.Request, cancellationToken);
        if (!result.IsValid)  
            return new EmployeeValidationError(result);

        return await next();
    }
}
