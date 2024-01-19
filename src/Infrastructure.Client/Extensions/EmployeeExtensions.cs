using Infrastructure.Client.Models;
using ProtoContracts;

namespace Infrastructure.Client.Extensions;

internal static class EmployeeExtensions
{
    public static List<Employee> FromResponse(this IEnumerable<EmployeeModel> response)
        => response.Select(employee => employee.FromResponse()).ToList();

    public static Employee FromResponse(this EmployeeModel employee)
    => new()
    {
        Id = employee.Id,
        FirstName = employee.Firstname,
        LastName = employee.Lastname,
        DepartmentId = employee.DepartmentId
    };
}



internal static class HistoryExtensions
{
    public static List<History> FromResponse(this IEnumerable<HistoryModel> response)
        => response.Select(history => history.FromResponse()).ToList();

    public static History FromResponse(this HistoryModel history)
    => new()
    {
        Id = history.Id,
        EmployeeId = history.EmployeeId,
        DepartmentId = history.DepartmentId,
        StartDate =history.StartDate,
        EndDate = history.EndDate
    };
}
