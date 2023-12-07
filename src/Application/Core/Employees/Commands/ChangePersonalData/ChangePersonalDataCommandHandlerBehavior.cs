using Core.Common;
using Core.Employees.Requests;
using Core.Employees.Errors;
using MediatR;

namespace Core.Employees.Commands.ChangePersonalData;

public class ChangePersonalDataCommandHandlerBehavior : IPipelineBehavior<ChangePersonalDataCommand, Result<EmployeeResultResponse>>
{
    public async Task<Result<EmployeeResultResponse>> Handle(ChangePersonalDataCommand request, RequestHandlerDelegate<Result<EmployeeResultResponse>> next, CancellationToken cancellationToken)
    {
        var validator = new ChangePersonalDataCommandRequestValidator();
        var result = await validator.ValidateAsync(request.Request, cancellationToken);
        if (!result.IsValid)
        {
            return EmployeeErrors.ValidationError(result);
        }
        return await next();
    }
}