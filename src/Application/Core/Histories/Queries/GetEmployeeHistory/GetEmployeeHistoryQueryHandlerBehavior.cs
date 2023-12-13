using static Core.Errors;
using ApplicationCore.Histories.Responses;
using Domain.Common;
using FluentValidation.Results;
using MediatR;

namespace ApplicationCore.Histories.Queries.GetEmployeeHistory;

public class GetEmployeeHistoryQueryHandlerBehavior : IPipelineBehavior<GetEmployeeHistoryQuery, Result<IEnumerable<HistoryResultResponse>>>
{
    public async Task<Result<IEnumerable<HistoryResultResponse>>> Handle(GetEmployeeHistoryQuery query, RequestHandlerDelegate<Result<IEnumerable<HistoryResultResponse>>> next, CancellationToken cancellationToken)
    {
        GetEmployeeHistoryQueryRequestValidator validator = new();
        ValidationResult result = await validator.ValidateAsync(query.Request, cancellationToken);
        if (!result.IsValid)  
            return new HistoryValidationError(result);
        return await next();
    }
}
