using Entities.Histories;
using Entities.Histories.ValueObjects;

namespace Persistence.DTO;

public class HistoryDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid DepartmentId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }

    public static HistoryDto FromDomain(History history)
    {
        return new()
        {
            Id = history.Id.Value,
            EmployeeId = history.EmployeeId.Value,
            DepartmentId = history.DepartmentId.Value,
            StartDate = history.StartDate,
            EndDate = history.EndDate
        };
    }

    public History ToDomain()
    {
        return History.Create(
            HistoryId.Create(Id),
            Entities.Employees.ValueObjects.EmployeeId.Create(EmployeeId),
            Entities.Departments.ValueObjects.DepartmentId.Create(DepartmentId),
            StartDate,
            EndDate
        );
    }
}
