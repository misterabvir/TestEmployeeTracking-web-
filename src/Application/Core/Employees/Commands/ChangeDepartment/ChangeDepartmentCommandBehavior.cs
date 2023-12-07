using Core.Common;
using Core.Employees.Requests;
using Core.Employees.Errors;
using MediatR;

namespace Core.Employees.Commands.ChangeDepartment;

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