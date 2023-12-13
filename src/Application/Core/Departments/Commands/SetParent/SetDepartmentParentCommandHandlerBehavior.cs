using Domain.Common;
using ApplicationCore.Departments.Responses;
using MediatR;
using static Core.Errors;

namespace ApplicationCore.Departments.Commands.SetParent;

public class SetDepartmentParentCommandHandlerBehavior : IPipelineBehavior<SetDepartmentParentCommand, Result<DepartmentResultResponse>>
{
    public async Task<Result<DepartmentResultResponse>> Handle(SetDepartmentParentCommand command, RequestHandlerDelegate<Result<DepartmentResultResponse>> next, CancellationToken cancellationToken)
    {
        var validator = new SetDepartmentParentCommandRequestValidator();
        var result = await validator.ValidateAsync(command.Request, cancellationToken);
        if (!result.IsValid)
            return new DepartmentValidationError(result);

        return await next();
    }
}
