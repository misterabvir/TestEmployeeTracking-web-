using Core.Abstractions.Common;
using Core.Common;
using Core.Employees.Requests;

namespace Core.Employees.Commands.Create;

public record CreateEmployeeCommand(CreateEmployeeCommandRequest Request) : ICommand<Result<EmployeeResultResponse>>;
