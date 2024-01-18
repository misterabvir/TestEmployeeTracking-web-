using ApplicationCore.Abstractions.Common;
using Domain.Common;
using ApplicationCore.Departments.Responses;

namespace ApplicationCore.Departments.Commands.ChangeTitle;

/// <summary>
/// Command for changing department title
/// </summary>
/// <param name="Request"> Request for changing department title </param>
public record ChangeDepartmentTitleCommand(ChangeDepartmentTitleCommandRequest Request) : ICommand<Result<DepartmentResultResponse>>;