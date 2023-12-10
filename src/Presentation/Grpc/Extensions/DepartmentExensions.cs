using ApplicationCore.Departments.Commands.ChangeTitle;
using ApplicationCore.Departments.Commands.Create;
using ApplicationCore.Departments.Commands.Delete;
using ApplicationCore.Departments.Commands.SetParent;
using ApplicationCore.Departments.Queries.GetById;
using ApplicationCore.Departments.Responses;

namespace Grpc.Extensions;

public static class DepartmentExensions
{
    public static DepartmentResponse ToResponse(
        this DepartmentResultResponse response)
        => new()
        {
            Id = response.Id.ToString(),
            Title = response.Title,
            ParentId = response.ParentId?.ToString() ?? ""
        };

    public static GetDepartmentByIdQuery ToResultQuery(
        this GetByIdDepartmentRequest request)
        => new(new(
            Guid.Parse(request.Id)));

    public static CreateDepartmentCommand ToResultCommand(
        this CreateDepartmentRequest request)
        => new(new(
              request.Title,
              string.IsNullOrWhiteSpace(request.ParentId) ? null : Guid.Parse(request.ParentId)));
    
    public static ChangeDepartmentTitleCommand ToResultCommand(
        this ChangeTitleDepartmentRequest request)
        => new(new(
            Guid.Parse(request.Id),
            request.NewTitle));

    public static SetDepartmentParentCommand ToResultCommand(
        this ChangeParentRequest request)
        => new(new(
            Guid.Parse(request.Id),
            string.IsNullOrWhiteSpace(request.NewParentId) ? null : Guid.Parse(request.NewParentId)));

    public static DeleteDepartmentCommand ToResultCommand(
        this DeleteDepartmentRequest request)
        => new(new(
            Guid.Parse(request.Id)));
            
}