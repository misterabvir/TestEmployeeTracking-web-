using Entities.Histories;

namespace ApplicationCore.Histories.Responses;

public sealed record HistoryResultResponse
{
    private HistoryResultResponse() { }
    public Guid Id { get; private set; }
    public Guid EmployeeId { get; private set; }
    public Guid DepartmentId { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; }

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