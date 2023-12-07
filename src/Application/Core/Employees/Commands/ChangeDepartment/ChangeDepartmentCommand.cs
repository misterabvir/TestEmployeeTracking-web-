using Core.Abstractions.Common;
using Core.Common;
using Core.Employees.Requests;

namespace Core.Employees.Commands.ChangeDepartment;

public record ChangeDepartmentCommand(ChangeDepartmentCommandRequest Request) : ICommand<Result<EmployeeResultResponse>>;
