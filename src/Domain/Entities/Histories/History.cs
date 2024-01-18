using Entities.Abstractions.General;
using Entities.Departments.ValueObjects;
using Entities.Employees.ValueObjects;
using Entities.Histories.ValueObjects;

namespace Entities.Histories;

/// <summary>
/// Domain entity of <see cref="History"/>
/// </summary>
public sealed class History : Entity<HistoryId>
{
    private History() { }
    /// <summary>
    /// Id of <see cref="Employee"/>
    /// </summary>
    public EmployeeId EmployeeId { get; private set; } = default!;
    /// <summary>
    /// Id of <see cref="Department"/>
    /// </summary> <summary>
    public DepartmentId DepartmentId { get; private set; } = default!;
    /// <summary>
    /// Date when <see cref="Employee"/> started working in <see cref="Department"/> 
    /// </summary>
    public DateOnly StartDate {  get; private set; }
    /// <summary>
    /// Date when <see cref="Employee"/> stopped working in <see cref="Department"/>
    /// </summary>
    public DateOnly? EndDate {  get; private set; }
    
    /// <summary>
    /// Create <see cref="History"/>
    /// </summary>
    /// <param name="employeeId"> Id of <see cref="Employee"/></param>
    /// <param name="departmentId"> Id of <see cref="Department"/></param>
    /// <param name="startDate"> Date when <see cref="Employee"/> started working in <see cref="Department"/></param>
    /// <param name="endDate"> Date  when <see cref="Employee"/> stopped working in <see cref="Department"/></param>
    /// <returns> Domain entity of <see cref="History"/></returns>
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
    /// <summary>
    /// Create <see cref="History"/>
    /// </summary>
    /// <param name="historyId"> Id of <see cref="History"/></param>
    /// <param name="employeeId"> Id of <see cref="Employee"/></param>
    /// <param name="departmentId"> Id of <see cref="Department"/></param>
    /// <param name="startDate"> Date when <see cref="Employee"/> started working in <see cref="Department"/></param>
    /// <param name="endDate"> Date  when <see cref="Employee"/> stopped working in <see cref="Department"/></param>
    /// <returns> Domain entity of <see cref="History"/></returns>
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

    /// <summary>
    /// Complete <see cref="History"/>
    /// </summary>
    /// <param name="endDate"> Date when <see cref="Employee"/> stopped working in <see cref="Department"/></param>
    internal void Complete(DateOnly endDate)
    {       
        EndDate = endDate;
    }
}
