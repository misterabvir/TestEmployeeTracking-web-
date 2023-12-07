using Core.Abstractions.Common;
using Core.Common;
using Core.Departments.Requests;

namespace Core.Departments.Commands.Create;

public record CreateDepartmentCommand(CreateDepartmentCommandRequest Request) : ICommand<Result<DepartmentResultResponse>>;

