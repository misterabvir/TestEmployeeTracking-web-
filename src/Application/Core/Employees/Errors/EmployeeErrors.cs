using Domain.Common;
using ApplicationCore.Employees.Responses;
using FluentValidation.Results;

namespace ApplicationCore.Employees.Errors;

public static class EmployeeErrors
{
    public static Error<EmployeeResultResponse> NotFound(Guid employeeId) =>
        new("Employee.GetById", $"Employee with id {{{employeeId}}} is not found", ResultErrorStatus.NotFound);

    public static Error<EmployeeResultResponse> DepartmentNotExist(Guid departmentId) =>
        new("Employee.Department", $"Department with id {{{departmentId}}} is not found", ResultErrorStatus.NotFound);

    public static Error<EmployeeResultResponse> ValidationError(ValidationResult result) =>
        new("Employee.Validation", $"Request is not valid: {string.Join(";\r\n", result.Errors.Select(err => $"{err.PropertyName} {err.ErrorMessage}"))}", ResultErrorStatus.InvalidArgument);

    public static Error<EmployeeResultResponse> AlreadyInDepartment(Guid employeeId) =>
        new("Employee.Department", $"Employee with id {{{employeeId}}} already in department", ResultErrorStatus.InvalidArgument);

    public static Error<EmployeeResultResponse> UnexpectedError(Error error) =>
        new(error.Title, error.Message, error.Status);

}
