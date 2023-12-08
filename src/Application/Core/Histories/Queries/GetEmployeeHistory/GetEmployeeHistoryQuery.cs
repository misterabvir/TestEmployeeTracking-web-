using ApplicationCore.Abstractions.Common;
using ApplicationCore.Histories.Responses;
using Domain.Common;

namespace ApplicationCore.Histories.Queries.GetEmployeeHistory;

public record class GetEmployeeHistoryQuery(GetEmployeeHistoryQueryRequest Request) : IQuery<Result<IEnumerable<HistoryResultResponse>>>;
