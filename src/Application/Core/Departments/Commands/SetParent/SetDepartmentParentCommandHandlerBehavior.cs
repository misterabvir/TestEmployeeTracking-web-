using Core.Common;
using Core.Departments.Errors;
using Core.Departments.Requests;
using MediatR;

namespace Core.Departments.Commands.SetParent;

public class SetDepartmentParentCommandHandlerBehavior : IPipelineBehavior<SetDepartmentParentCommand, Result<DepartmentResultResponse>>
{
    public async Task<Result<DepartmentResultResponse>> Handle(SetDepartmentParentCommand command, RequestHandlerDelegate<Result<DepartmentResultResponse>> next, CancellationToken cancellationToken)
    {
        var validator = new SetDepartmentParentCommandRequestValidator();
        var result = await validator.ValidateAsync(command.Request, cancellationToken);
        if (!result.IsValid)
            return DepartmentErrors.ValidationError(result);

        return await next();
    }
}