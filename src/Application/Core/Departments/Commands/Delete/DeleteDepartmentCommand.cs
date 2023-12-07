using Core.Abstractions.Common;
using Core.Common;

namespace Core.Departments.Commands.Delete;

public record DeleteDepartmentCommand(DeleteDepartmentCommandRequest Request) : ICommand<Result>;
