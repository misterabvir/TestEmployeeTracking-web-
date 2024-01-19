using ApplicationCore.Employees.Responses;
using Domain.Common;
using FluentValidation.Results;

namespace Core;

public partial class Errors
{
    public record EmployeeNotFoundError(Guid EmployeeId) :
        Error<EmployeeResultResponse>(
            "Employee.GetById",
            $"Employee with id {{{EmployeeId}}} is not found",
            ResultErrorStatus.NotFound);
    
    public record EmployeeDepartmentNotExistError(Guid DepartmentId) :
        Error<EmployeeResultResponse>(
            "Employee.Department",
            $"Department with id {{{DepartmentId}}} is not found",
            ResultErrorStatus.NotFound);

    public record EmployeeDepartmentNotFoundError(Guid DepartmentId) :
        Error<IEnumerable<EmployeeResultResponse>>(
            "Employee.Department",
            $"Department with id {{{DepartmentId}}} is not found",
            ResultErrorStatus.NotFound);

    public record EmployeeValidationError(ValidationResult Result) :
        Error<EmployeeResultResponse>(
            "Employee.Validation",
            $"Request is not valid: {string.Join(";\r\n", Result.Errors.Select(err => $"{err.PropertyName} {err.ErrorMessage}"))}",
            ResultErrorStatus.InvalidArgument);
    
    public record EmployeeAlreadyInDepartmentError(Guid EmployeeId) :
        Error<EmployeeResultResponse>(
            "Employee.Department",
            $"Employee with id {{{EmployeeId}}} already in department",
            ResultErrorStatus.InvalidArgument);
    
    public record EmployeeUnexpectedError(Error Error) :
        Error<EmployeeResultResponse>(
            Error.Title,
            Error.Message,
            Error.Status);
}
