using ApplicationCore.Departments.Responses;
using Domain.Common;
using FluentValidation.Results;

namespace Core;

public partial class Errors
{
    public record DepartmentNotFoundError(Guid DepartmentId) :
        Error<DepartmentResultResponse>(
            "Department.NotFound",
            $"Department with id {DepartmentId} is not exist",
            ResultErrorStatus.InvalidArgument);

    public record DepartmentValidationError(ValidationResult Result) :
        Error<DepartmentResultResponse>(
            "Department.Validation",
            $"Request is not valid: {string.Join(";\r\n", Result.Errors.Select(err => $"{err.PropertyName} {err.ErrorMessage}"))}",
            ResultErrorStatus.InvalidArgument);

    public record DepartmentAlreadyRootError(Guid DepartmentId) :
        Error<DepartmentResultResponse>(
            "Department.AlreadyRoot",
            $"Department with id {DepartmentId} is already root",
            ResultErrorStatus.BadRequest);

    public record DepartmentCantDeleteNotEmptyError(Guid DepartmentId) :
        Error(
            "Department.NotEmpty",
            $"Can't delete department with id {{{DepartmentId}}}, because some employee worked in this department",
            ResultErrorStatus.BadRequest);

    public record DepartmentUnexpectedError(Error Error) :
        Error<DepartmentResultResponse>(
            Error.Title,
            Error.Message,
            Error.Status);

    public record DepartmentAlreadyExistError(Guid DepartmentId) :
        Error<DepartmentResultResponse>(
            "Department.AlreadyExist",
            $"Department with same name already exist i parent with id {{{DepartmentId}}}",
            ResultErrorStatus.BadRequest);

    public record DepartmentAndParentSameError(Guid DepartmentId) :
        Error<DepartmentResultResponse>(
            "Department.DepartmentAndParentSame",
            $"Department  with id {{{DepartmentId}}} can,t be itself parent",
            ResultErrorStatus.BadRequest);

    public record DepartmentParentNotFoundError(Guid DepartmentId) :
        Error<DepartmentResultResponse>(
            "Department.ParentDepartmentNotFound",
            $"Parent department with id {{{DepartmentId}}} not found",
            ResultErrorStatus.BadRequest);
}
