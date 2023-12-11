using Domain.Common;
using Entities.Histories;

namespace Entities.Abstractions.Services;

public interface IHistoryService
{
    Result<History> Complete(History history, DateOnly endDate);
}
