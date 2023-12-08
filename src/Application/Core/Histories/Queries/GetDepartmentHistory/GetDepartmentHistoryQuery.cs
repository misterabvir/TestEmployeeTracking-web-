using ApplicationCore.Abstractions.Common;
using ApplicationCore.Histories.Responses;
using Domain.Common;

namespace ApplicationCore.Histories.Queries.GetDepartmentHistory;

public record GetDepartmentHistoryQuery(GetDepartmentHistoryQueryRequest Request) : IQuery<Result<IEnumerable<HistoryResultResponse>>>;
