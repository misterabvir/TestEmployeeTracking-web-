using ApplicationCore.Abstractions.Common;
using Domain.Common;

namespace ApplicationCore.Departments.Commands.Delete;

public record DeleteDepartmentCommand(DeleteDepartmentCommandRequest Request) : ICommand<Result>;
