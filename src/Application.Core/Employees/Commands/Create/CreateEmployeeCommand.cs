using ApplicationCore.Abstractions.Common;
using Domain.Common;
using ApplicationCore.Employees.Responses;

namespace ApplicationCore.Employees.Commands.Create;

/// <summary>
/// Command to create new employee
/// </summary>
/// <param name="Request"> Request with new employee data </param>
public record CreateEmployeeCommand(CreateEmployeeCommandRequest Request) : ICommand<Result<EmployeeResultResponse>>;
