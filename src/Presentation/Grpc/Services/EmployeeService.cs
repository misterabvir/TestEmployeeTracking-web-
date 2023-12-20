using ApplicationCore.Employees.Queries.GetAll;
using Grpc.Core;
using Grpc.Extensions;
using MediatR;

namespace Grpc.Services;

public class EmployeeService : EmployeesProto.EmployeesProtoBase
{
    private readonly ISender _sender;

    public EmployeeService(ISender sender)
    {
        _sender = sender;
    }

    public override async Task<MultipleEmployeeResponse> GetAll(EmployeeAllRequest request, ServerCallContext context)
    {
        var result = await _sender.Send(new GetAllEmployeeQuery());
        if (result.IsFailure)
        {
            throw new RpcException(result.Error.GetStatus());
        }

        return result.Value!.ToResponse();
    }

    public override async Task<EmployeeResponse> GetById(
        EmployeeByIdRequest request,
        ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultQuery());
        if (result.IsFailure)
        {
            throw new RpcException(result.Error.GetStatus());
        }
        return result.Value!.ToResponse();
    }

    public override async Task<EmployeeResponse> Create(
        EmployeeCreateRequest request,
        ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultCommand());
        if (result.IsFailure)
        {
            throw new RpcException(result.Error.GetStatus());
        }
        return result.Value!.ToResponse();
    }

    public async override Task<EmployeeResponse> ChangePersonalData(
        EmployeeChangePersonalDataRequest request,
        ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultCommand());
        if (result.IsFailure)
        {
            throw new RpcException(result.Error.GetStatus());
        }
        return result.Value!.ToResponse();
    }

    public async override Task<EmployeeResponse> ChangeDepartment(
        EmployeeChangeDepartmentRequest request,
        ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultCommand());
        if (result.IsFailure)
        {
            throw new RpcException(result.Error.GetStatus());
        }
        return result.Value!.ToResponse();
    }

    public async override Task<EmployeeEmptyResponse> Delete(EmployeeDeleteRequest request, ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultCommand());
        if (result.IsFailure)
        {
            throw new RpcException(result.Error.GetStatus());
        }
        return new EmployeeEmptyResponse();
    }
}
