using ApplicationCore.Histories.Queries.GetDepartmentHistory;
using ApplicationCore.Histories.Queries.GetEmployeeHistory;
using ApplicationCore.Histories.Responses;
using Domain.Common;
using Grpc.Protos;

namespace Grpc.Extensions;

public static class HistoryExtensions
{

    public static HistoryResultMultipleResponse ToResponse(
      this Result<IEnumerable<HistoryResultResponse>> result)
    {
        var reply = new HistoryResultMultipleResponse();
        if ((reply.IsSucces = result.IsSuccess))
        {
            reply.Histories.AddRange(result.Value!.Select(d => d.ToResponse()));
        }
        else
        {
            reply.Error = result.Error.ToErrorModel();
        }
        return reply;
    }


    public static HistoryModel ToResponse(
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
        this HistoryDepartmentRequest request)
        => new(new(
            Guid.Parse(request.DepartmentId)));

    public static GetEmployeeHistoryQuery ToResultQuery(
    this HistoryEmployeeRequest request)
        => new(new(
            Guid.Parse(request.EmployeeId)));
}