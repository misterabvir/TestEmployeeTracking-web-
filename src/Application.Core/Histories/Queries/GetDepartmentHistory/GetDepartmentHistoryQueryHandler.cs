using ApplicationCore.Abstractions.Common;
using ApplicationCore.Abstractions.Repositories;
using static Core.Errors;
using ApplicationCore.Histories.Responses;
using Domain.Common;
using Entities.Departments;
using Entities.Departments.ValueObjects;

namespace ApplicationCore.Histories.Queries.GetDepartmentHistory;

/// <summary>
/// Handler for get department history
/// </summary>
public class GetDepartmentHistoryQueryHandler : IQueryHandler<GetDepartmentHistoryQuery, Result<IEnumerable<HistoryResultResponse>>>
{
    /// <summary>
    /// Repository for <see cref="History"/>
    /// </summary>
    private readonly IHistoryRepository _historyRepository;
    /// <summary>
    /// Repository for <see cref="Department"/>
    /// </summary>
    private readonly IDepartmentRepository _departmentRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDepartmentHistoryQueryHandler"/> class.
    /// </summary>
    /// <param name="historyRepository"> Repository for <see cref="History"/></param>
    /// <param name="departmentRepository"> Repository for <see cref="Department"/> </param>
    public GetDepartmentHistoryQueryHandler(
        IHistoryRepository historyRepository, 
        IDepartmentRepository departmentRepository)
    {
        _historyRepository = historyRepository;
        _departmentRepository = departmentRepository;
    }

    /// <summary>
    /// Handler for get department history
    /// </summary>
    /// <param name="query"> Query to get department history </param>
    /// <param name="cancellationToken"> Cancellation token </param>
    /// <returns> Result with list of department history or error </returns>
    public async Task<Result<IEnumerable<HistoryResultResponse>>> Handle(
        GetDepartmentHistoryQuery query, CancellationToken cancellationToken)
    {
        // Check if department exists
        DepartmentId departmentId = DepartmentId.Create(query.Request.DepartmentId);
        Department? department = await _departmentRepository.Get(departmentId, cancellationToken);
        if(department is null)
        {    
            return new HistoryDepartmentNotFoundError(departmentId.Value);
        }

        // Get department history
        var result = await _historyRepository.GetDepartmentHistory(departmentId, cancellationToken);
        return Result<IEnumerable<HistoryResultResponse>>.Success(result.Select(x=>HistoryResultResponse.FromDomain(x)));
    }
}
