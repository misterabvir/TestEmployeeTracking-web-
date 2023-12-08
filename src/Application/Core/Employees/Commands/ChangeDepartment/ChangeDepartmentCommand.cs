using ApplicationCore.Abstractions.Common;
using Domain.Common;
using ApplicationCore.Employees.Responses;

namespace ApplicationCore.Employees.Commands.ChangeDepartment;

public record ChangeDepartmentCommand(ChangeDepartmentCommandRequest Request) : ICommand<Result<EmployeeResultResponse>>;
