using Entities.Histories;

namespace ApplicationCore.Histories.Responses;

/// <summary>
/// Response for history
/// </summary>
public sealed record HistoryResultResponse
{
    private HistoryResultResponse() { }
    
    /// <summary>
    /// Id of history
    /// </summary>
    public Guid Id { get; private set; }
    /// <summary>
    /// Id of employee
    /// </summary>
    public Guid EmployeeId { get; private set; }
    /// <summary>
    /// Id of department
    /// </summary>
    public Guid DepartmentId { get; private set; }
    /// <summary>
    /// Date the employee started working in this department
    /// </summary>
    public DateOnly StartDate { get; private set; }
    /// <summary>
    /// Date the employee stopped working in this department
    /// </summary>
    public DateOnly? EndDate { get; private set; }

    /// <summary>
    /// Creates new instance of <see cref="HistoryResultResponse"/> from domain entity
    /// </summary>
    /// <param name="history"> Domain entity of history to convert </param>
    /// <returns> Instance of <see cref="HistoryResultResponse"/> </returns>
    internal static HistoryResultResponse FromDomain(History history)
    {
        return new()
        {
            Id = history.Id.Value,
            EmployeeId = history.EmployeeId.Value,
            DepartmentId = history.DepartmentId.Value,
            StartDate = history.StartDate,
            EndDate = history.EndDate,
        };
    }
}