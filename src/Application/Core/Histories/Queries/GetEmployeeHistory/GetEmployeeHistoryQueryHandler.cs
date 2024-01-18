using ApplicationCore.Abstractions.Common;
using ApplicationCore.Abstractions.Repositories;
using static Core.Errors;
using ApplicationCore.Histories.Responses;
using Domain.Common;
using Entities.Employees;
using Entities.Employees.ValueObjects;

namespace ApplicationCore.Histories.Queries.GetEmployeeHistory;

/// <summary>
/// Handler for get employee history
/// </summary>
public class GetEmployeeHistoryQueryHandler : IQueryHandler<GetEmployeeHistoryQuery, Result<IEnumerable<HistoryResultResponse>>>
{
    /// <summary>
    /// Repository for <see cref="History"/>
    /// </summary>
    private readonly IHistoryRepository _historyRepository;
    /// <summary>
    /// Repository for <see cref="Employee"/>
    /// </summary>
    private readonly IEmployeeRepository _employeeRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetEmployeeHistoryQueryHandler"/> class.
    /// </summary>
    /// <param name="historyRepository"> Repository for <see cref="History"/> </param>
    /// <param name="employeeRepository"> Repository for <see cref="Employee"/> </param>
    public GetEmployeeHistoryQueryHandler(
        IHistoryRepository historyRepository,
        IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
        _historyRepository = historyRepository;
    }

    /// <summary>
    /// Handler for get employee history
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Result<IEnumerable<HistoryResultResponse>>> Handle(
        GetEmployeeHistoryQuery query, CancellationToken cancellationToken)
    {
        // Check if employee exists
        EmployeeId employeeId = EmployeeId.Create(query.Request.EmployeeId);
        Employee? employee = await _employeeRepository.Get(employeeId, cancellationToken);
        if (employee is null)
        {
            return new HistoryEmployeeNotFoundError(employeeId.Value);
        }

        // Get employee history
        var result = await _historyRepository.GetEmployeeHistory(employeeId, cancellationToken);
        return Result<IEnumerable<HistoryResultResponse>>.Success(result.Select(HistoryResultResponse.FromDomain));
    }
}
