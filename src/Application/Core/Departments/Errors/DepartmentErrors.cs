using Domain.Common;
using ApplicationCore.Departments.Responses;
using FluentValidation.Results;

namespace ApplicationCore.Departments.Errors;

public class DepartmentErrors
{
    public static Error<DepartmentResultResponse> NotFound(Guid id) =>
        new("Department.NotFound", $"Department with id {id} is not exist");

    public static Error<DepartmentResultResponse> ValidationError(ValidationResult result) =>
        new("Department.Validation", $"Request is not valid: {string.Join(";\r\n", result.Errors.Select(err => $"{err.PropertyName} {err.ErrorMessage}"))}");

    public static Error<DepartmentResultResponse> AlreadyRoot(Guid value)=>
        new("Department.AlreadyRoot", $"Department with id {value} is already root");

    public static Error CantDeleteNotEmptyDepartment(Guid value)=>
        new ("Department.NotEmpty", $"Can't delete department with id {{{value}}}, because some employee worked in this department");

    public static Error<DepartmentResultResponse> Unexpected(Guid value) =>   
        new("Department.Unexpected",  $"Unexpected error with employee with id {{{value}}}");

    public static Error<DepartmentResultResponse> AlreadyExist(Guid value) =>
        new("Department.AlreadyExist",  $"Department with same name and parent already exist with id {{{value}}}");

}