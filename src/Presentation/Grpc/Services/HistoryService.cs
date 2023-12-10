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

    public override async Task GetEmployeeHistory(
        EmployeeHistoryRequest request, 
        IServerStreamWriter<HistoryResponse> responseStream, 
        ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultQuery());
        if (result.IsFailure)
        {
            throw new RpcException(result.Error.GetStatus());
        }

        var data = result.Value!.Select(x => x.ToResponse());

        foreach (var row in data)
        {
            await responseStream.WriteAsync(row);
        }
    }

    public override async Task GetDepartmentHistory(
        DepartmentHistoryRequest request, 
        IServerStreamWriter<HistoryResponse> responseStream, 
        ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultQuery());
        if (result.IsFailure)
        {
            throw new RpcException(result.Error.GetStatus());
        }

        var data = result.Value!.Select(x => x.ToResponse());

        foreach (var row in data)
        {
            await responseStream.WriteAsync(row);
        }
    }


}