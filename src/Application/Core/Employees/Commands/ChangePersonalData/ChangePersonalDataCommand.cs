using ApplicationCore.Abstractions.Common;
using Domain.Common;
using ApplicationCore.Employees.Responses;

namespace ApplicationCore.Employees.Commands.ChangePersonalData;

/// <summary>
/// Command to change personal data
/// </summary>
/// <param name="Request"> Request with employee id and new personal data </param>
public record ChangePersonalDataCommand(ChangePersonalDataCommandRequest Request) : ICommand<Result<EmployeeResultResponse>>;
