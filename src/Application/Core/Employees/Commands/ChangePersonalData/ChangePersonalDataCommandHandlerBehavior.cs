using Domain.Common;
using ApplicationCore.Employees.Responses;
using static Core.Errors;
using MediatR;

namespace ApplicationCore.Employees.Commands.ChangePersonalData;

public class ChangePersonalDataCommandHandlerBehavior : IPipelineBehavior<ChangePersonalDataCommand, Result<EmployeeResultResponse>>
{
    public async Task<Result<EmployeeResultResponse>> Handle(ChangePersonalDataCommand request, RequestHandlerDelegate<Result<EmployeeResultResponse>> next, CancellationToken cancellationToken)
    {
        var validator = new ChangePersonalDataCommandRequestValidator();
        var result = await validator.ValidateAsync(request.Request, cancellationToken);
        if (!result.IsValid)
        {
            return new EmployeeValidationError(result);
        }
        return await next();
    }
}