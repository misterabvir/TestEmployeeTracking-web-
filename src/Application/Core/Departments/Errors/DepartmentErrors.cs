using Core.Common;
using Core.Departments.Requests;
using FluentValidation.Results;

namespace Core.Departments.Errors;

public class DepartmentErrors
{
    public static Error<DepartmentResultResponse> NotFound(Guid id) =>
        new("Department.NotFound", $"Department with id {id} is not exist");

    public static Error<DepartmentResultResponse> ValidationError(ValidationResult result) =>
        new("Employee.Validation", $"Request is not valid: {string.Join(";\r\n", result.Errors.Select(err => $"{err.PropertyName} {err.ErrorMessage}"))}");

    public static Error<DepartmentResultResponse> AlreadyRoot(Guid value)=>
        new("Department.AlreadyRoot", $"Department with id {value} is already root");

    public static Error CantDeleteNotEmptyDepartment(Guid value)=>
        new ("Department.NotEmpty", $"Can't delete department with id {{{value}}}, because some employee worked in this department");
}