using Domain.Common;
using ApplicationCore.Departments.Errors;
using ApplicationCore.Departments.Responses;
using MediatR;

namespace ApplicationCore.Departments.Queries.GetById;

public class GetDepartmentByIdQueryHandlerBehavior : IPipelineBehavior<GetDepartmentByIdQuery, Result<DepartmentResultResponse>>
{
    public async Task<Result<DepartmentResultResponse>> Handle(GetDepartmentByIdQuery request, RequestHandlerDelegate<Result<DepartmentResultResponse>> next, CancellationToken cancellationToken)
    {
        var validator = new GetDepartmentByIdQueryRequestValidator();
        var result = await validator.ValidateAsync(request.Request, cancellationToken);
        if (!result.IsValid)  
            return DepartmentErrors.ValidationError(result);

        return await next();
    }
}
