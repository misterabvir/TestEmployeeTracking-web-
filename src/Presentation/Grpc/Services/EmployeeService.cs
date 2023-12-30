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

    public override async Task<EmployeeResultMulitpleResponse> GetAll(EmployeeAllRequest request, ServerCallContext context)
    {
        var result = await _sender.Send(new GetAllEmployeeQuery());
        return result.ToResponse();
    }

    public override async Task<EmployeeResultSingleResponse> GetById(
        EmployeeByIdRequest request,
        ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultQuery());
        return result.ToResponse();
    }

    public override async Task<EmployeeResultSingleResponse> Create(
        EmployeeCreateRequest request,
        ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultCommand());
        return result.ToResponse();
    }

    public async override Task<EmployeeResultSingleResponse> ChangePersonalData(
        EmployeeChangePersonalDataRequest request,
        ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultCommand());
        return result.ToResponse();
    }

    public async override Task<EmployeeResultSingleResponse> ChangeDepartment(
        EmployeeChangeDepartmentRequest request,
        ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultCommand());
        return result.ToResponse();
    }

    public async override Task<EmployeeResultEmptyResponse> Delete(EmployeeDeleteRequest request, ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultCommand());
        return new EmployeeResultEmptyResponse()
        {
            IsSucces = result.IsSuccess,
            Error = result.IsSuccess ? new() : new()
            {
                Status = result.Error.Status.ToString(),
                Title = result.Error!.Title,
                Description = result.Error!.Message
            }
        };
    }
}
