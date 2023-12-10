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

    public override async Task GetAll(
        AllEmployeeRequest request,
        IServerStreamWriter<EmployeeResponse> responseStream,
        ServerCallContext context)
    {
        var result = await _sender.Send(new GetAllEmployeeQuery());
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

    public override async Task<EmployeeResponse> GetById(
        ByIdEmployeeRequest request, 
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
        CreateEmployeeRequest request,
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
        ChangePersonalDataEmployeeRequest request,
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
        ChangeDepartmentEmployeeRequest request,
        ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultCommand());
        if (result.IsFailure)
        {
            throw new RpcException(result.Error.GetStatus());
        }
        return result.Value!.ToResponse();
    }

    public async override Task<EmployeeDeleteResponse> Delete(DeleteEmployeeRequest request, ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultCommand());
        if (result.IsFailure)
        {
            throw new RpcException(result.Error.GetStatus());
        }
        return new EmployeeDeleteResponse();
    }
}
