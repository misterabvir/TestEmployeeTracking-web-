using ApplicationCore.Histories.Responses;
using Domain.Common;
using FluentValidation.Results;

namespace Core;

public partial class Errors
{
    public record HistoryDepartmentNotFoundError(Guid DepartmentId) :  Error<IEnumerable<HistoryResultResponse>>("History.DepartmentNotFound", $"Department with id {DepartmentId} not found", ResultErrorStatus.NotFound);
    public record HistoryEmployeeNotFoundError(Guid EmployeeId) :  Error<IEnumerable<HistoryResultResponse>>("History.EmployeeNotFound", $"Employee with id {EmployeeId} not found", ResultErrorStatus.NotFound);
    public record HistoryValidationError(ValidationResult Result) :  Error<IEnumerable<HistoryResultResponse>>("History.Validation", $"Request is not valid: {string.Join(";\r\n", Result.Errors.Select(err => $"{err.PropertyName} {err.ErrorMessage}"))}", ResultErrorStatus.InvalidArgument);
}