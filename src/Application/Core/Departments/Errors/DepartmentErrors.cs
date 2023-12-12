using Domain.Common;
using ApplicationCore.Departments.Responses;
using FluentValidation.Results;

namespace ApplicationCore.Departments.Errors;

public class DepartmentErrors
{
    public static Error<DepartmentResultResponse> NotFound(Guid id) =>
        new("Department.NotFound", $"Department with id {id} is not exist", ResultErrorStatus.InvalidArgument);

    public static Error<DepartmentResultResponse> ValidationError(ValidationResult result) =>
        new("Department.Validation", $"Request is not valid: {string.Join(";\r\n", result.Errors.Select(err => $"{err.PropertyName} {err.ErrorMessage}"))}", ResultErrorStatus.InvalidArgument);

    public static Error<DepartmentResultResponse> AlreadyRoot(Guid value)=>
        new("Department.AlreadyRoot", $"Department with id {value} is already root", ResultErrorStatus.BadRequest);

    public static Error CantDeleteNotEmptyDepartment(Guid value)=>
        new ("Department.NotEmpty", $"Can't delete department with id {{{value}}}, because some employee worked in this department", ResultErrorStatus.BadRequest);

    public static Error<DepartmentResultResponse> Unexpected(Error error) =>   
        new(error.Title, error.Message, error.Status);

    public static Error<DepartmentResultResponse> AlreadyExist(Guid value) =>
        new("Department.AlreadyExist",  $"Department with same name already exist i parent with id {{{value}}}", ResultErrorStatus.BadRequest);

    public static Error<DepartmentResultResponse> DepartmentAndParentSame(Guid value) =>
        new("Department.DepartmentAndParentSame", $"Department  with id {{{value}}} can,t be itself parent", ResultErrorStatus.BadRequest);

    public static Error<DepartmentResultResponse> ParentDepartmentNotFound(Guid value)
        => new("Department.ParentDepartmentNotFound", $"Parent department with id {{{value}}} not found", ResultErrorStatus.BadRequest);
}