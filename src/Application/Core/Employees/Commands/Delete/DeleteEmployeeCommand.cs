using ApplicationCore.Abstractions.Common;
using Domain.Common;

namespace ApplicationCore.Employees.Commands.Delete;

public record DeleteEmployeeCommand(DeleteEmployeeCommandRequest Request) : ICommand<Result>;