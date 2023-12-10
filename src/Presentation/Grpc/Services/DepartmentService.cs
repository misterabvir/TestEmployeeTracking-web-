using ApplicationCore.Departments.Queries.GetAll;
using Grpc.Core;
using Grpc.Extensions;
using MediatR;

namespace Grpc.Services;

public sealed class DepartmentService : DepartmentsProto.DepartmentsProtoBase
{
    private readonly ISender _sender;

    public DepartmentService(ISender sender)
    {
        _sender = sender;
    }

    public override async Task GetAll(
        GetAllDepartmentRequest request, 
        IServerStreamWriter<DepartmentResponse> responseStream, 
        ServerCallContext context)
    {
        var result = await _sender.Send(new GetAllDepartmentsQuery());
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

    public override async Task<DepartmentResponse> GetById(
        GetByIdDepartmentRequest request, 
        ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultQuery());
        if (result.IsFailure)
        {
            throw new RpcException(result.Error.GetStatus());
        }
        return result.Value!.ToResponse();
    }

    public override async Task<DepartmentResponse> Create(
        CreateDepartmentRequest request, 
        ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultCommand());
        if (result.IsFailure)
        {
            throw new RpcException(result.Error.GetStatus());
        }
        return result.Value!.ToResponse();
    }

    public override async Task<DepartmentResponse> ChangeTitle(
        ChangeTitleDepartmentRequest request, 
        ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultCommand());
        if (result.IsFailure)
        {
            throw new RpcException(result.Error.GetStatus());
        }
        return result.Value!.ToResponse();
    }

    public override async Task<DepartmentResponse> ChangeParent(
        ChangeParentRequest request, 
        ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultCommand());
        if (result.IsFailure)
        {
            throw new RpcException(result.Error.GetStatus());
        }
        return result.Value!.ToResponse();
    }

    public override async Task<DepartmentResponse> Delete(
        DeleteDepartmentRequest request, 
        ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultCommand());
        if (result.IsFailure)
        {
            throw new RpcException(result.Error.GetStatus());
        }
        return new DepartmentResponse();
    }
}
