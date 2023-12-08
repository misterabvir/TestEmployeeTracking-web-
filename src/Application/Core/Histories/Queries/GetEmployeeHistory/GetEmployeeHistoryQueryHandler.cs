using ApplicationCore.Abstractions.Common;
using ApplicationCore.Abstractions.Repositories;
using ApplicationCore.Histories.Errors;
using ApplicationCore.Histories.Responses;
using Domain.Common;
using Entities.Employees;
using Entities.Employees.ValueObjects;

namespace ApplicationCore.Histories.Queries.GetEmployeeHistory;

public class GetEmployeeHistoryQueryHandler : 
    IQueryHandler<GetEmployeeHistoryQuery, Result<IEnumerable<HistoryResultResponse>>>
{
    private readonly IHistoryRepository _historyRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeeHistoryQueryHandler(
        IEmployeeRepository employeeRepository, 
        IHistoryRepository historyRepository)
    {
        _employeeRepository = employeeRepository;
        _historyRepository = historyRepository;
    }

    public async Task<Result<IEnumerable<HistoryResultResponse>>> Handle(
        GetEmployeeHistoryQuery query, CancellationToken cancellationToken)
    {
        EmployeeId employeeId = EmployeeId.Create(query.Request.EmployeeId);
        Employee? employee = await _employeeRepository.Get(employeeId, cancellationToken);
        if(employee is null)
        {
            return HistoryErrors.EmployeeNotFound(employeeId.Value);
        }
        var result = await _historyRepository.GetEmployeeHistory(employeeId, cancellationToken);
        return Result<IEnumerable<HistoryResultResponse>>.Success(result.Select(HistoryResultResponse.FromDomain));
    }
}