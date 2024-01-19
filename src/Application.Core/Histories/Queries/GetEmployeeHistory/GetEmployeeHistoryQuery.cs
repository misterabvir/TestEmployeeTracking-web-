using ApplicationCore.Abstractions.Common;
using ApplicationCore.Histories.Responses;
using Domain.Common;

namespace ApplicationCore.Histories.Queries.GetEmployeeHistory;
/// <summary>
/// Query to get employee history
/// </summary>
/// <param name="Request"> Request with employee id </param>
public record class GetEmployeeHistoryQuery(GetEmployeeHistoryQueryRequest Request) : IQuery<Result<IEnumerable<HistoryResultResponse>>>;
