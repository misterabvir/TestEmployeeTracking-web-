using Domain.Common;
using ApplicationCore.Departments.Errors;
using ApplicationCore.Departments.Responses;
using MediatR;

namespace ApplicationCore.Departments.Commands.ChangeTitle;

public class ChangeDepartmentTitleCommandHandlerBehavior : IPipelineBehavior<ChangeDepartmentTitleCommand, Result<DepartmentResultResponse>>
{
    public async Task<Result<DepartmentResultResponse>> Handle(ChangeDepartmentTitleCommand command, RequestHandlerDelegate<Result<DepartmentResultResponse>> next, CancellationToken cancellationToken)
    {
        var validator = new ChangeDepartmentTitleCommandRequestValidator();
        var result = await validator.ValidateAsync(command.Request, cancellationToken);
        if (!result.IsValid)
            return DepartmentErrors.ValidationError(result);

        return await next();
    }
}
