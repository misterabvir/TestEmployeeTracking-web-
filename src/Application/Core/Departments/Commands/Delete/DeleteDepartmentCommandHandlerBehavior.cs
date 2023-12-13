using Domain.Common;
using static Core.Errors;
using MediatR;

namespace ApplicationCore.Departments.Commands.Delete;

public class DeleteDepartmentCommandHandlerBehavior : IPipelineBehavior<DeleteDepartmentCommand, Result>
{
    public async Task<Result> Handle(DeleteDepartmentCommand command, RequestHandlerDelegate<Result> next, CancellationToken cancellationToken)
    {
        var validator = new DeleteDepartmentCommandRequestValidator();
        var result = await validator.ValidateAsync(command.Request, cancellationToken);
        if (!result.IsValid)  
            return (Error)new DepartmentValidationError(result);

        return await next();
    }
}
