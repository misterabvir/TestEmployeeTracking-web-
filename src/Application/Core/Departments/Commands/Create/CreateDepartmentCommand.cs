using ApplicationCore.Abstractions.Common;
using Domain.Common;
using ApplicationCore.Departments.Responses;

namespace ApplicationCore.Departments.Commands.Create;

/// <summary>
/// Command for creating department
/// </summary>
/// <param name="Request">Request for creating department</param>
public record CreateDepartmentCommand(CreateDepartmentCommandRequest Request) : ICommand<Result<DepartmentResultResponse>>;

