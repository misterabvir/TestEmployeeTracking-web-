using ApplicationCore.Employees.Commands.ChangeDepartment;
using ApplicationCore.Employees.Commands.ChangePersonalData;
using ApplicationCore.Employees.Commands.Create;
using ApplicationCore.Employees.Commands.Delete;
using ApplicationCore.Employees.Queries.GetById;
using ApplicationCore.Employees.Responses;

namespace Grpc.Extensions;

public static class EmployeeExtension
{
    public static MultipleEmployeeResponse ToResponse(this IEnumerable<EmployeeResultResponse> response)
    {
        var reply = new MultipleEmployeeResponse();
        reply.Employees.AddRange(response.Select(d => d.ToResponse()));
        return reply;
    }

    public static EmployeeResponse ToResponse(
        this EmployeeResultResponse resultResponse)
        => new()
        {
            Id = resultResponse.Id.ToString(),
            Lastname = resultResponse.LastName,
            Firstname = resultResponse.FirstName,
            DepartmentId = resultResponse.DepartmentId.ToString()
        };

    public static GetEmployeeByIdQuery ToResultQuery(
        this EmployeeByIdRequest request)
        => new(new(
            Guid.Parse(request.Id)));

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
