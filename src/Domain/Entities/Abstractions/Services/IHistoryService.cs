using Domain.Common;
using Entities.Histories;

namespace Entities.Abstractions.Services;

/// <summary>
/// Service for changing <see cref="History"/>
/// </summary>
public interface IHistoryService
{
    /// <summary>
    /// Completes <see cref="History"/>
    /// </summary>
    /// <param name="history"> History to complete </param>
    /// <param name="endDate"> Date the employee stopped working </param>
    /// <returns></returns>
    Result<History> Complete(History history, DateOnly endDate);
}
