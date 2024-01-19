using Domain.Common;

namespace Entities.Histories;

/// <summary>
/// History domain errors
/// </summary>
public static class HistoryDomainErrors
{
    /// <summary>
    /// Error when <see cref="History.EndDate"/> is less than <see cref="History.StartDate"/>
    /// </summary>
    /// <returns> Error when <see cref="History.EndDate"/> is less than <see cref="History.StartDate"/></returns>
    public static Error<History> EndDateLessStartDate => new("History.EndDate", "EndDate must be greater or equal than startDate", ResultErrorStatus.InvalidArgument);
}