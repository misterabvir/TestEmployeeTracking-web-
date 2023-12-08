using ApplicationCore.Abstractions.Common;
using ApplicationCore.Abstractions.Repositories;
using Domain.Common;
using ApplicationCore.Employees.Responses;
using Entities.Employees;

namespace ApplicationCore.Employees.Queries.GetAll;

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