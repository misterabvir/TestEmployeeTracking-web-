﻿using Domain.Common;
using ApplicationCore.Employees.Responses;
using FluentValidation.Results;

namespace ApplicationCore.Employees.Errors;

public static class EmployeeErrors
{
    public static Error<EmployeeResultResponse> NotFound(Guid employeeId) =>
        new("Employee.GetById", $"Employee with id {{{employeeId}}} is not found");

    public static Error<EmployeeResultResponse> DepartmentNotExist(Guid departmentId) =>
        new("Employee.Department", $"Department with id {{{departmentId}}} is not found");

    public static Error<EmployeeResultResponse> ValidationError(ValidationResult result) =>
        new("Employee.Validation", $"Request is not valid: {string.Join(";\r\n", result.Errors.Select(err => $"{err.PropertyName} {err.ErrorMessage}"))}");

    public static Error<EmployeeResultResponse> AlreadyInDepartment(Guid employeeId) =>
        new("Employee.Department", $"Employee with id {{{employeeId}}} already in department");

    public static Error<EmployeeResultResponse> UnexpectedError(Guid employeeId) =>
        new("Employee.Unexpected", $"Unexpected error with employee with id {{{employeeId}}}");

}
