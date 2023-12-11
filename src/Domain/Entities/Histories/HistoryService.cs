using Domain.Common;
using Entities.Abstractions.Services;

namespace Entities.Histories;

internal class HistoryService : IHistoryService
{

    public Result<History> Complete(History history, DateOnly endDate)
    {
        if(history.StartDate < endDate)
        {
            return HistoryDomainErrors.EndDateMustBeGreaterOrEqualStartDate;
        }
        history.Complete(endDate);
        return Result<History>.Success(history);
    }
}
