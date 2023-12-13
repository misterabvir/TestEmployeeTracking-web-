using Domain.Common;
using ApplicationCore.Departments.Responses;
using MediatR;
using static Core.Errors;

namespace ApplicationCore.Departments.Commands.ChangeTitle;

public class ChangeDepartmentTitleCommandHandlerBehavior : IPipelineBehavior<ChangeDepartmentTitleCommand, Result<DepartmentResultResponse>>
{
    public async Task<Result<DepartmentResultResponse>> Handle(ChangeDepartmentTitleCommand command, RequestHandlerDelegate<Result<DepartmentResultResponse>> next, CancellationToken cancellationToken)
    {
        var validator = new ChangeDepartmentTitleCommandRequestValidator();
        var result = await validator.ValidateAsync(command.Request, cancellationToken);
        if (!result.IsValid)
            return new DepartmentValidationError(result);
        return await next();
    }
}
