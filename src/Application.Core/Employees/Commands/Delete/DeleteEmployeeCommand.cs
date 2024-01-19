using ApplicationCore.Abstractions.Common;
using Domain.Common;

namespace ApplicationCore.Employees.Commands.Delete;

/// <summary>
/// Command to delete employee
/// </summary>
/// <param name="Request"> Request with employee id </param>
public record DeleteEmployeeCommand(DeleteEmployeeCommandRequest Request) : ICommand<Result>;