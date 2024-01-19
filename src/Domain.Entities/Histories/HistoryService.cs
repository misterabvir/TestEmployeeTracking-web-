using Domain.Common;
using Entities.Abstractions.Services;

namespace Entities.Histories;

/// <summary>
/// Service for changing <see cref="History"/>
/// </summary>
internal class HistoryService : IHistoryService
{
    /// <summary>
    /// Complete <see cref="History"/>
    /// </summary>
    /// <param name="history">Domain entity of <see cref="History"/></param>
    /// <param name="endDate">Date when <see cref="History"/> stopped working in <see cref="Department"/> for complete</param>
    /// <returns> Result of operation </returns>
    public Result<History> Complete(History history, DateOnly endDate)
    {
        if(history.StartDate > endDate)
        {
            return HistoryDomainErrors.EndDateLessStartDate;
        }
        history.Complete(endDate);
        return Result<History>.Success(history);
    }
}
