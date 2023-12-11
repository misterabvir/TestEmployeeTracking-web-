using Entities.Abstractions;
using Entities.Departments.ValueObjects;
using Entities.Employees.ValueObjects;
using Entities.Histories.ValueObjects;

namespace Entities.Histories;

public sealed class History : Entity<HistoryId>
{
    private History() { }
    public EmployeeId EmployeeId { get; private set; } = default!;
    public DepartmentId DepartmentId { get; private set; } = default!;
    public DateOnly StartDate {  get; private set; }
    public DateOnly? EndDate {  get; private set; }
    public static History Create(EmployeeId employeeId,
        DepartmentId departmentId,
        DateOnly startDate, 
        DateOnly? endDate = null)
    {
        return new()
        {
            Id = HistoryId.CreateUnique(),
            EmployeeId = employeeId,
            DepartmentId = departmentId,
            StartDate = startDate,
            EndDate = endDate
        };
    }

    public static History Create(HistoryId historyId,
        EmployeeId employeeId,
       DepartmentId departmentId,
       DateOnly startDate,
       DateOnly? endDate = null)
    {
        return new()
        {
            Id = historyId,
            EmployeeId = employeeId,
            DepartmentId = departmentId,
            StartDate = startDate,
            EndDate = endDate
        };
    }

    internal void Complete(DateOnly endDate)
    {       
        EndDate = endDate;
    }
}
