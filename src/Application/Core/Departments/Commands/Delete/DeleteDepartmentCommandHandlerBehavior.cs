using Core.Common;
using Core.Departments.Errors;
using MediatR;

namespace Core.Departments.Commands.Delete;

public class DeleteDepartmentCommandHandlerBehavior : IPipelineBehavior<DeleteDepartmentCommand, Result>
{
    public async Task<Result> Handle(DeleteDepartmentCommand command, RequestHandlerDelegate<Result> next, CancellationToken cancellationToken)
    {
        var validator = new DeleteDepartmentCommandRequestValidator();
        var result = await validator.ValidateAsync(command.Request, cancellationToken);
        if (!result.IsValid)  
            return (Error)DepartmentErrors.ValidationError(result);

        return await next();
    }
}
