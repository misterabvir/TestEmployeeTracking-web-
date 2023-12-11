using Domain.Common;

namespace Entities.Histories;

public static class HistoryDomainErrors
{
    public static Error<History> EndDateMustBeGreaterOrEqualStartDate => new("History.EndDate", "EndDate must be greater or equal than startDate", ResultErrorStatus.InvalidArgument);
}