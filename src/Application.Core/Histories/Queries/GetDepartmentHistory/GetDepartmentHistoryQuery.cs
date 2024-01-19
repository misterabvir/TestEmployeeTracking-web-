using ApplicationCore.Abstractions.Common;
using ApplicationCore.Histories.Responses;
using Domain.Common;

namespace ApplicationCore.Histories.Queries.GetDepartmentHistory;
/// <summary>
/// Query to get department history
/// </summary>
/// <param name="Request"> Request with department id </param>
public record GetDepartmentHistoryQuery(GetDepartmentHistoryQueryRequest Request) : IQuery<Result<IEnumerable<HistoryResultResponse>>>;
