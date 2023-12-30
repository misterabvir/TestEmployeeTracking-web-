using Grpc.Core;
using Grpc.Extensions;
using MediatR;

namespace Grpc.Services;

public sealed class HistoryService : HistoriesProto.HistoriesProtoBase
{
    private readonly ISender _sender;

    public HistoryService(ISender sender)
    {
        _sender = sender;
    }

    public override async Task<HistoryResultMultipleResponse> GetDepartmentHistory(HistoryDepartmentRequest request, ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultQuery());
        return result.ToResponse();
    }


    public override async Task<HistoryResultMultipleResponse> GetEmployeeHistory(HistoryEmployeeRequest request, ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultQuery());
        return result.ToResponse();
    }
}