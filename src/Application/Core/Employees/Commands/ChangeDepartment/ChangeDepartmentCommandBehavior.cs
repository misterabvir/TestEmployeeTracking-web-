using Domain.Common;
using ApplicationCore.Employees.Responses;
using ApplicationCore.Employees.Errors;
using MediatR;

namespace ApplicationCore.Employees.Commands.ChangeDepartment;

public class ChangeDepartmentCommandBehavior : IPipelineBehavior<ChangeDepartmentCommand, Result<EmployeeResultResponse>>
{
    public async Task<Result<EmployeeResultResponse>> Handle(ChangeDepartmentCommand command, RequestHandlerDelegate<Result<EmployeeResultResponse>> next, CancellationToken cancellationToken)
    {
        var validator = new ChangeDepartmentCommandRequestValidator();
        var result = await validator.ValidateAsync(command.Request, cancellationToken);
        if (!result.IsValid)
            return EmployeeErrors.ValidationError(result);

        return await next();
    }
}