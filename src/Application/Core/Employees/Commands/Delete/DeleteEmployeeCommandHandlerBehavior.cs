using Domain.Common;
using static Core.Errors;
using MediatR;

namespace ApplicationCore.Employees.Commands.Delete;

public class DeleteEmployeeCommandHandlerBehavior : IPipelineBehavior<DeleteEmployeeCommand, Result>
{
    public async Task<Result> Handle(DeleteEmployeeCommand command,
                                     RequestHandlerDelegate<Result> next,
                                     CancellationToken cancellationToken)
    {
        var validator = new DeleteEmployeeCommandRequestValidator();
        var result = await validator.ValidateAsync(command.Request, cancellationToken);
        if (!result.IsValid)
            return (Error)new EmployeeValidationError(result);
        return await next();
    }
}
