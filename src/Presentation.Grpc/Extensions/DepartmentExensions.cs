using ApplicationCore.Departments.Commands.ChangeTitle;
using ApplicationCore.Departments.Commands.Create;
using ApplicationCore.Departments.Commands.Delete;
using ApplicationCore.Departments.Commands.SetParent;
using ApplicationCore.Departments.Queries.GetById;
using ApplicationCore.Departments.Responses;
using Domain.Common;
using ProtoContracts;

namespace Grpc.Extensions;

public static class DepartmentExensions
{
    public static DepartmentResultMulitpleResponse ToResponse(this Result<IEnumerable<DepartmentResultResponse>> result)
    {
        var reply = new DepartmentResultMulitpleResponse();
        if ((reply.IsSucces = result.IsSuccess))
        {           
            reply.Departments.AddRange(result.Value!.Select(d => d.ToResponse()));
        }
        else
        {
            reply.Error = result.Error.ToErrorModel();
        }
        return reply;
    }

    public static DepartmentResultSingleResponse ToResponse(this Result<DepartmentResultResponse> result)
    {
        var reply = new DepartmentResultSingleResponse();
        if ((reply.IsSucces = result.IsSuccess))
        {
            reply.Department = result.Value!.ToResponse();
        }
        else
        {
            reply.Error = result.Error.ToErrorModel();
        }
        return reply;
    }

    public static DepartmentModel ToResponse(
        this DepartmentResultResponse response)
        => new()
        {
            Id = response.Id.ToString(),
            Title = response.Title,
            ParentId = response.ParentId?.ToString() ?? ""
        };

    public static GetDepartmentByIdQuery ToResultQuery(
        this DepartmentGetByIdRequest request)
        => new(new(
            Guid.Parse(request.Id)));

    public static CreateDepartmentCommand ToResultCommand(
        this DepartmentCreateRequest request)
        => new(new(
              request.Title,
              string.IsNullOrWhiteSpace(request.ParentId) ? null : Guid.Parse(request.ParentId)));

    public static ChangeDepartmentTitleCommand ToResultCommand(
        this DepartmentChangeTitleRequest request)
        => new(new(
            Guid.Parse(request.Id),
            request.NewTitle));

    public static SetDepartmentParentCommand ToResultCommand(
        this DepartmentChangeParentRequest request)
        => new(new(
            Guid.Parse(request.Id),
            string.IsNullOrWhiteSpace(request.NewParentId) ? null : Guid.Parse(request.NewParentId)));

    public static DeleteDepartmentCommand ToResultCommand(
        this DepartmentDeleteRequest request)
        => new(new(
            Guid.Parse(request.Id)));

}