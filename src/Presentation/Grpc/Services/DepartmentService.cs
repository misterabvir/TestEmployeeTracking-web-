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

    public override async Task<MultipleDepartmentResponse> GetAll(DepartmentGetAllRequest request, ServerCallContext context)
    {
        var result = await _sender.Send(new GetAllDepartmentsQuery());
        if (result.IsFailure)
        {
            throw new RpcException(result.Error.GetStatus());
        }
        return result.Value!.ToResponse();
    }

    public override async Task<DepartmentResponse> GetById(
        DepartmentGetByIdRequest request, 
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
        DepartmentCreateRequest request, 
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
        DepartmentChangeTitleRequest request, 
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
        DepartmentChangeParentRequest request, 
        ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultCommand());
        if (result.IsFailure)
        {
            throw new RpcException(result.Error.GetStatus());
        }
        return result.Value!.ToResponse();
    }

    public override async Task<DepartmentEmptyResponse> Delete(
        DepartmentDeleteRequest request, 
        ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultCommand());
        if (result.IsFailure)
        {
            throw new RpcException(result.Error.GetStatus());
        }
        return new DepartmentEmptyResponse();
    }
}
