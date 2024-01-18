using ApplicationCore.Abstractions.Common;
using Domain.Common;
using ApplicationCore.Employees.Responses;

namespace ApplicationCore.Employees.Commands.ChangeDepartment;

/// <summary>
/// Command to change department
/// </summary>
/// <param name="Request"> Request with employee id and new department id </param>
public record ChangeDepartmentCommand(ChangeDepartmentCommandRequest Request) : ICommand<Result<EmployeeResultResponse>>;
