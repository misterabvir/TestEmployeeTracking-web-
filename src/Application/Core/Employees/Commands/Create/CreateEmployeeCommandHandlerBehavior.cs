using Core.Common;
using Core.Employees.Requests;
using Core.Employees.Errors;
using MediatR;

namespace Core.Employees.Commands.Create;

public class CreateEmployeeCommandHandlerBehavior
    : IPipelineBehavior<CreateEmployeeCommand, Result<EmployeeResultResponse>>
{
    public async Task<Result<EmployeeResultResponse>> Handle(CreateEmployeeCommand command, RequestHandlerDelegate<Result<EmployeeResultResponse>> next, CancellationToken cancellationToken)
    {
        var validator = new CreateEmployeeCommandRequestValidator();
        var result = await validator.ValidateAsync(command.Request, cancellationToken);
        if (!result.IsValid)
            return EmployeeErrors.ValidationError(result);

        return await next();
    }
}