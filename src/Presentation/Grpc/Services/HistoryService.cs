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

    public override async Task<MultipleHistoryResponse> GetDepartmentHistory(DepartmentHistoryRequest request, ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultQuery());
        if (result.IsFailure)
        {
            throw new RpcException(result.Error.GetStatus());
        }
        var reply = new MultipleHistoryResponse();
        reply.Histories.AddRange(result.Value!.Select(x => x.ToResponse()));
        return reply;
    }


    public override async Task<MultipleHistoryResponse> GetEmployeeHistory(EmployeeHistoryRequest request, ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultQuery());
        if (result.IsFailure)
        {
            throw new RpcException(result.Error.GetStatus());
        }
        var reply = new MultipleHistoryResponse();
        reply.Histories.AddRange(result.Value!.Select(x => x.ToResponse()));
        return reply;
    }
}