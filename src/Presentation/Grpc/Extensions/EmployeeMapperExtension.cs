using ApplicationCore.Employees.Commands.ChangeDepartment;
using ApplicationCore.Employees.Commands.ChangePersonalData;
using ApplicationCore.Employees.Commands.Create;
using ApplicationCore.Employees.Commands.Delete;
using ApplicationCore.Employees.Queries.GetById;
using ApplicationCore.Employees.Responses;
using ApplicationCore.Histories.Queries.GetDepartmentHistory;
using ApplicationCore.Histories.Queries.GetEmployeeHistory;
using ApplicationCore.Histories.Responses;
using System.Runtime.CompilerServices;

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

    public static GetEmployeeByIdQuery ToResultQuery(
        this ByIdEmployeeRequest request)
        => new(new(
            Guid.Parse(request.Id)));

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

    public static DeleteEmployeeCommand ToResultCommand(
        this DeleteEmployeeRequest request)
        => new(new(
            Guid.Parse(request.Id)));
}

public static class HistoryExtensions
{
    public static HistoryResponse ToResponse(
        this HistoryResultResponse result)
        => new()
        { 
             Id = result.Id.ToString(),
              DepartmentId = result.DepartmentId.ToString(),
              EmployeeId = result.EmployeeId.ToString(),
              StartDate = result.StartDate.ToShortDateString(),
              EndDate = result.EndDate?.ToShortDateString() ?? string.Empty
        };
    
    public static GetDepartmentHistoryQuery ToResultQuery(
        this DepartmentHistoryRequest request)
        => new(new(
            Guid.Parse(request.DepartmentId)));

    public static GetEmployeeHistoryQuery ToResultQuery(
    this EmployeeHistoryRequest request)
        => new(new(
            Guid.Parse(request.EmployeeId)));
}