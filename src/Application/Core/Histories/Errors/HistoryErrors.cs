using ApplicationCore.Histories.Responses;
using Domain.Common;
using Entities.Departments.ValueObjects;
using FluentValidation.Results;

namespace ApplicationCore.Histories.Errors;

public static class HistoryErrors
{
    public static Error<IEnumerable<HistoryResultResponse>> ValidationError(ValidationResult result) =>
        new("History.Validation", $"Request is not valid: {string.Join(";\r\n", result.Errors.Select(err => $"{err.PropertyName} {err.ErrorMessage}"))}");

    public static Error<IEnumerable<HistoryResultResponse>> DepartmentNotFound(DepartmentId departmentId) =>
        new("History.DepartmentNotFound", $"Department with id {departmentId.Value} not found");

    public static Error<IEnumerable<HistoryResultResponse>> EmployeeNotFound(Guid value) =>
       new("History.EmployeeNotFound", $"Employee with id {value} not found"); 
}