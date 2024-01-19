using Application.Core.Employees.Queries.GetByDepartmentId;
using ApplicationCore.Employees.Commands.ChangeDepartment;
using ApplicationCore.Employees.Commands.ChangePersonalData;
using ApplicationCore.Employees.Commands.Create;
using ApplicationCore.Employees.Commands.Delete;
using ApplicationCore.Employees.Queries.GetById;
using ApplicationCore.Employees.Responses;
using Domain.Common;
using ProtoContracts;

namespace Grpc.Extensions;

public static class EmployeeExtension
{
    public static EmployeeResultMulitpleResponse ToResponse(this Result<IEnumerable<EmployeeResultResponse>> result)
    {
               
        var reply = new EmployeeResultMulitpleResponse();
        if ((reply.IsSucces = result.IsSuccess))
        {
            reply.Employees.AddRange(result.Value!.Select(d => d.ToResponse()));
        }
        else
        {
            reply.Error = result.Error.ToErrorModel();
        }
        return reply;
    }

    public static EmployeeResultSingleResponse ToResponse(this Result<EmployeeResultResponse> result)
    {

        var reply = new EmployeeResultSingleResponse();
        if ((reply.IsSucces = result.IsSuccess))
        {
            reply.Employee = result.Value!.ToResponse();
        }
        else
        {
            reply.Error = result.Error.ToErrorModel();
        }
        return reply;
    }


    public static EmployeeModel ToResponse(
        this EmployeeResultResponse resultResponse)
        => new()
        {
            Id = resultResponse.Id.ToString(),
            Lastname = resultResponse.LastName,
            Firstname = resultResponse.FirstName,
            DepartmentId = resultResponse.DepartmentId.ToString()
        };

    public static GetEmployeesByDepartmentIdQuery ToResultQuery(this EmployeeByDepartmentIdRequest request)
    => new(new(Guid.Parse(request.Id)));

    public static GetEmployeeByIdQuery ToResultQuery(this EmployeeByIdRequest request)
        => new(new(Guid.Parse(request.Id)));

    public static CreateEmployeeCommand ToResultCommand(
        this EmployeeCreateRequest request)
        => new(new(
            request.Lastname,
            request.Firstname,
            Guid.Parse(request.DepartmentId)));

    public static ChangePersonalDataCommand ToResultCommand(
        this EmployeeChangePersonalDataRequest request)
        => new(new(
             Guid.Parse(request.Id),
             request.NewLastname,
             request.NewFirstname));

    public static ChangeDepartmentCommand ToResultCommand(
        this EmployeeChangeDepartmentRequest request)
        => new(new(
            Guid.Parse(request.Id),
            Guid.Parse(request.NewDepartmentId)));

    public static DeleteEmployeeCommand ToResultCommand(
        this EmployeeDeleteRequest request)
        => new(new(
            Guid.Parse(request.Id)));
}
