using Core.Abstractions.Common;
using Core.Common;

namespace Core.Employees.Commands.Delete;

public record DeleteEmployeeCommand(DeleteEmployeeCommandRequest Request) : ICommand<Result>;