using static Core.Errors;
using ApplicationCore.Histories.Responses;
using Domain.Common;
using FluentValidation.Results;
using MediatR;

namespace ApplicationCore.Histories.Queries.GetDepartmentHistory;

public class GetDepartmentHistoryQueryBehavior : IPipelineBehavior<GetDepartmentHistoryQuery, Result<IEnumerable<HistoryResultResponse>>>
{
    public async Task<Result<IEnumerable<HistoryResultResponse>>> Handle(GetDepartmentHistoryQuery query, RequestHandlerDelegate<Result<IEnumerable<HistoryResultResponse>>> next, CancellationToken cancellationToken)
    {
        GetDepartmentHistoryQueryRequestValidator validator = new();
        ValidationResult result = await validator.ValidateAsync(query.Request, cancellationToken);
        if (!result.IsValid)  
            return new HistoryValidationError(result);
        return await next();
    }
}
