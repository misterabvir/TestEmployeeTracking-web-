using Domain.Common;
using ApplicationCore.Employees.Responses;
using ApplicationCore.Employees.Errors;
using MediatR;

namespace ApplicationCore.Employees.Commands.Create;

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