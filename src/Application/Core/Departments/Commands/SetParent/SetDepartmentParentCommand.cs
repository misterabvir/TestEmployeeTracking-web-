using ApplicationCore.Abstractions.Common;
using Domain.Common;
using ApplicationCore.Departments.Responses;

namespace ApplicationCore.Departments.Commands.SetParent;

/// <summary>
/// Command for setting parent department
/// </summary>
/// <param name="Request"> Request for setting parent department </param>
public record SetDepartmentParentCommand(SetDepartmentParentCommandRequest Request) : ICommand<Result<DepartmentResultResponse>>;
