using ApplicationCore.Abstractions.Common;
using ApplicationCore.Abstractions.Repositories;
using ApplicationCore.Histories.Errors;
using ApplicationCore.Histories.Responses;
using Domain.Common;
using Entities.Departments;
using Entities.Departments.ValueObjects;

namespace ApplicationCore.Histories.Queries.GetDepartmentHistory;

public class GetDepartmentHistoryQueryHandler : IQueryHandler<GetDepartmentHistoryQuery, Result<IEnumerable<HistoryResultResponse>>>
{
    private readonly IHistoryRepository _historyRepository;
    private readonly IDepartmentRepository _departmentRepository;

    public GetDepartmentHistoryQueryHandler(
        IHistoryRepository historyRepository, 
        IDepartmentRepository departmentRepository)
    {
        _historyRepository = historyRepository;
        _departmentRepository = departmentRepository;
    }

    public async Task<Result<IEnumerable<HistoryResultResponse>>> Handle(
        GetDepartmentHistoryQuery query, CancellationToken cancellationToken)
    {
        DepartmentId departmentId = DepartmentId.Create(query.Request.DepartmentId);
        Department? department = await _departmentRepository.Get(departmentId, cancellationToken);
        if(department == null)
        {    
            return HistoryErrors.DepartmentNotFound(departmentId);
        }
        var result = await _historyRepository.GetDepartmentHistory(departmentId, cancellationToken);
        return Result<IEnumerable<HistoryResultResponse>>.Success(result.Select(x=>HistoryResultResponse.FromDomain(x)));
    }
}
