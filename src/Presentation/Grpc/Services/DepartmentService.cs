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

    public override async Task<DepartmentResultMulitpleResponse> GetAll(DepartmentGetAllRequest request, ServerCallContext context)
    {
        var result = await _sender.Send(new GetAllDepartmentsQuery());
        return result.ToResponse();
    }

    public override async Task<DepartmentResultSingleResponse> GetById(
        DepartmentGetByIdRequest request, 
        ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultQuery());
        return result.ToResponse();
    }

    public override async Task<DepartmentResultSingleResponse> Create(
        DepartmentCreateRequest request, 
        ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultCommand());
        return result.ToResponse();
    }

    public override async Task<DepartmentResultSingleResponse> ChangeTitle(
        DepartmentChangeTitleRequest request, 
        ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultCommand());
        return result.ToResponse();
    }

    public override async Task<DepartmentResultSingleResponse> ChangeParent(
        DepartmentChangeParentRequest request, 
        ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultCommand());
        return result.ToResponse();
    }

    public override async Task<DepartmentResultEmptyResponse> Delete(
        DepartmentDeleteRequest request, 
        ServerCallContext context)
    {
        var result = await _sender.Send(request.ToResultCommand());

        return new DepartmentResultEmptyResponse()
        {
            IsSucces = result.IsSuccess,
            Error = result.Error is null ? new() : new()
            {
                Status = result.Error.Status.ToString(),
                Title = result.Error!.Title,
                Description = result.Error!.Message
            }
        };
    }
}
