using ApplicationCore.Abstractions.Common;
using ApplicationCore.Abstractions.Repositories;
using Domain.Common;
using ApplicationCore.Employees.Responses;
using ApplicationCore.Employees.Errors;
using Entities.Employees.ValueObjects;

namespace ApplicationCore.Employees.Queries.GetById;

public class GetEmployeeByIdQueryHandler : IQueryHandler<GetEmployeeByIdQuery, Result<EmployeeResultResponse>>
{
    private readonly IEmployeeRepository _repository;

    public GetEmployeeByIdQueryHandler(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<EmployeeResultResponse>> Handle(
        GetEmployeeByIdQuery query,
        CancellationToken cancellationToken)
    {
        EmployeeId employeeId = EmployeeId.Create(query.Request.EmployeeId);
        var result = await _repository.Get(employeeId, cancellationToken);
        if (result is null)
        {
            return EmployeeErrors.NotFound(query.Request.EmployeeId);
        }

        return Result<EmployeeResultResponse>.Success(EmployeeResultResponse.FromDomain(result));
    }
}
