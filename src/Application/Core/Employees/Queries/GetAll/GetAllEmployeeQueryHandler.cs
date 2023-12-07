using Core.Abstractions.Common;
using Core.Abstractions.Repositories;
using Core.Common;
using Core.Employees.Requests;
using Entities.Employees;

namespace Core.Employees.Queries.GetAll;

public sealed class GetAllEmployeeQueryHandler : IQueryHandler<GetAllEmployeeQuery, Result<IEnumerable<EmployeeResultResponse>>>
{
    public readonly IEmployeeRepository _repository;

    public GetAllEmployeeQueryHandler(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<EmployeeResultResponse>>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Employee> employees = await _repository.Get(cancellationToken);        
        return Result<IEnumerable<EmployeeResultResponse>>.Success(employees.Select(EmployeeResultResponse.FromDomain));
    }
}