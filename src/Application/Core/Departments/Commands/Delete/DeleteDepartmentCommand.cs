using ApplicationCore.Abstractions.Common;
using Domain.Common;

namespace ApplicationCore.Departments.Commands.Delete;

/// <summary>
/// Command for deleting department
/// </summary>
/// <param name="Request"> Request for deleting department </param>
public record DeleteDepartmentCommand(DeleteDepartmentCommandRequest Request) : ICommand<Result>;
