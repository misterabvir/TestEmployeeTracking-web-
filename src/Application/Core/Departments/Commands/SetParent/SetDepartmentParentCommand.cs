using Core.Abstractions.Common;
using Core.Common;
using Core.Departments.Requests;

namespace Core.Departments.Commands.SetParent;

public record SetDepartmentParentCommand(SetDepartmentParentCommandRequest Request) : ICommand<Result<DepartmentResultResponse>>;
