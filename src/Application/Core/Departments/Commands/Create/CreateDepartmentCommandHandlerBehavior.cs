using Domain.Common;
using static Core.Errors;
using ApplicationCore.Departments.Responses;
using MediatR;

namespace ApplicationCore.Departments.Commands.Create;

public class CreateDepartmentCommandHandlerBehavior : IPipelineBehavior<CreateDepartmentCommand, Result<DepartmentResultResponse>>
{
    public async Task<Result<DepartmentResultResponse>> Handle(CreateDepartmentCommand request, RequestHandlerDelegate<Result<DepartmentResultResponse>> next, CancellationToken cancellationToken)
    {
        var validator = new CreateDepartmentCommandRequestValidator();
        var result = await validator.ValidateAsync(request.Request, cancellationToken);
        if (!result.IsValid)
            return new DepartmentValidationError(result);

        return await next();
    }
}
