namespace ApplicationCore.Employees.Commands.ChangePersonalData;

/// <summary>
/// Request with employee id and new personal data
/// </summary>
/// <param name="EmployeeId"> Id of employee to change </param>
/// <param name="LastName"> New last name </param>
/// <param name="FirstName"> New first name </param>
public record ChangePersonalDataCommandRequest(Guid EmployeeId, string LastName, string FirstName);
