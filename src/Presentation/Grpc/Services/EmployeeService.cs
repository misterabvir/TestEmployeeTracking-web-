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
            throw new RpcException(Status.DefaultCancelled, result.Error.Description);
        }

        var employees = result.Value!.Select(x => new EmployeeResponse()
        {
            Id = x.Id.ToString(),
            Lastname = x.LastName,
            Firstname = x.FirstName,
            DepartmentId = x.DepartmentId.ToString()
        });

        foreach (var employee in employees)
        { 
            await responseStream.WriteAsync(employee);
        }
    }

    public override async Task<EmployeeResponse> Create(
        CreateEmployeeRequest request, 
        ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultCommand());
        if (result.IsFailure)
        {
            throw new RpcException(Status.DefaultCancelled, result.Error.Description);
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
            throw new RpcException(Status.DefaultCancelled, result.Error.Description);
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
            throw new RpcException(Status.DefaultCancelled, result.Error.Description);
        }
        return result.Value!.ToResponse();
    }
}

