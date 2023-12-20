using ApplicationCore.Histories.Queries.GetDepartmentHistory;
using ApplicationCore.Histories.Queries.GetEmployeeHistory;
using ApplicationCore.Histories.Responses;

namespace Grpc.Extensions;

public static class HistoryExtensions
{

    public static MultipleHistoryResponse ToResponse(
      this IEnumerable<HistoryResultResponse> response)
    {
        MultipleHistoryResponse reply = new();
        reply.Histories.AddRange(response.Select(h => h.ToResponse()));
        return reply;
    }


    public static HistoryResponse ToResponse(
        this HistoryResultResponse result)
        => new()
        {
            Id = result.Id.ToString(),
            DepartmentId = result.DepartmentId.ToString(),
            EmployeeId = result.EmployeeId.ToString(),
            StartDate = result.StartDate.ToShortDateString(),
            EndDate = result.EndDate?.ToShortDateString() ?? string.Empty
        };

    public static GetDepartmentHistoryQuery ToResultQuery(
        this DepartmentHistoryRequest request)
        => new(new(
            Guid.Parse(request.DepartmentId)));

    public static GetEmployeeHistoryQuery ToResultQuery(
    this EmployeeHistoryRequest request)
        => new(new(
            Guid.Parse(request.EmployeeId)));
}