using ApplicationCore.Abstractions.Common;
using ApplicationCore.Abstractions.Repositories;
using static Core.Errors;
using ApplicationCore.Employees.Responses;
using Domain.Common;
using Entities.Employees.ValueObjects;

namespace ApplicationCore.Employees.Queries.GetById;

/// <summary>
/// Handler for get employee by id
/// </summary>
public class GetEmployeeByIdQueryHandler : IQueryHandler<GetEmployeeByIdQuery, Result<EmployeeResultResponse>>
{
    /// <summary>
    /// Repository for <see cref="Employee"/>
    /// </summary>
    private readonly IEmployeeRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetEmployeeByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="repository"> Repository for <see cref="Employee"/></param>
    public GetEmployeeByIdQueryHandler(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Handler for get employee by id
    /// </summary>
    /// <param name="query"> Query to get employee by id </param>
    /// <param name="cancellationToken"> Cancellation token </param>
    /// <returns> Result with employee or error </returns>
    public async Task<Result<EmployeeResultResponse>> Handle(
        GetEmployeeByIdQuery query,
        CancellationToken cancellationToken)
    {
        // Check if employee exists
        EmployeeId employeeId = EmployeeId.Create(query.Request.EmployeeId);
        var result = await _repository.Get(employeeId, cancellationToken);
        if (result is null)
        {
            return new EmployeeNotFoundError(query.Request.EmployeeId);
        }

        return Result<EmployeeResultResponse>.Success(EmployeeResultResponse.FromDomain(result));
    }
}
