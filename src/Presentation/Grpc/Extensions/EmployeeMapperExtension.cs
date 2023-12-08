using ApplicationCore.Employees.Commands.ChangeDepartment;
using ApplicationCore.Employees.Commands.ChangePersonalData;
using ApplicationCore.Employees.Commands.Create;
using ApplicationCore.Employees.Responses;

namespace Grpc.Extensions;

public static class EmployeeMapperExtension
{
    public static EmployeeResponse ToResponse(
        this EmployeeResultResponse resultResponse)
        => new()
        {
            Id = resultResponse.Id.ToString(),
            Lastname = resultResponse.LastName,
            Firstname = resultResponse.FirstName,
            DepartmentId = resultResponse.DepartmentId.ToString()
        };

    public static CreateEmployeeCommand ToResultCommand(
        this CreateEmployeeRequest request)
        => new(new(
            request.Lastname,
            request.Firstname,
            Guid.Parse(request.DepartmentId)));

    public static ChangePersonalDataCommand ToResultCommand(
        this ChangePersonalDataEmployeeRequest request)
        => new(new(
             Guid.Parse(request.Id),
             request.NewLastname,
             request.NewFirstname));

    public static ChangeDepartmentCommand ToResultCommand(
        this ChangeDepartmentEmployeeRequest request)
        => new(new(
            Guid.Parse(request.Id),
            Guid.Parse(request.NewDepartmentId)));


}
