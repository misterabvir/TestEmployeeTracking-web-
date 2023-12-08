using ApplicationCore.Abstractions.Common;
using Domain.Common;
using ApplicationCore.Employees.Responses;

namespace ApplicationCore.Employees.Commands.Create;

public record CreateEmployeeCommand(CreateEmployeeCommandRequest Request) : ICommand<Result<EmployeeResultResponse>>;
