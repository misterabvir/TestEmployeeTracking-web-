using ApplicationCore.Abstractions.Common;
using ApplicationCore.Abstractions.Repositories;
using ApplicationCore.Employees.Responses;
using Core;
using Domain.Common;
using Entities.Departments;
using Entities.Departments.ValueObjects;

namespace Application.Core.Employees.Queries.GetByDepartmentId;

/// <summary>
/// Hanler for qery get employees in <see cref="Department"/>
/// </summary>
public class GetEmployeesByDepartmentIdQueryHandler : IQueryHandler<GetEmployeesByDepartmentIdQuery, Result<IEnumerable<EmployeeResultResponse>>>
{
    /// <summary>
    /// Repository of <see cref="Department"/>
    /// </summary>
    private readonly IDepartmentRepository _departmentRepository;
    /// <summary>
    /// Repositoy of <see cref="Employee"/>
    /// </summary>
    private readonly IEmployeeRepository _employeeRepository;

    /// <summary>
    /// Initialize new instance <see cref="GetEmployeesByDepartmentId"/>
    /// </summary>
    /// <param name="employeeRepository">Repositoy of <see cref="Employee"/></param>
    /// <param name="departmentRepository">Repository of <see cref="Department"/></param>
    public GetEmployeesByDepartmentIdQueryHandler(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
    {
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
    }

    /// <summary>
    /// Hanler for qery get employees in <see cref="Department"/>
    /// </summary>
    /// <param name="query"> Query for get employees in department</param>
    /// <param name="cancellationToken"> Cancellation token </param>
    /// <returns>List of employees</returns>
    public async Task<Result<IEnumerable<EmployeeResultResponse>>> Handle(GetEmployeesByDepartmentIdQuery query, CancellationToken cancellationToken)
    {
        // Check department is existing
        DepartmentId departmentId = DepartmentId.Create(query.Request.DepartmentId);
        Department? department = await _departmentRepository.Get(departmentId, cancellationToken);
        if(department is null)
        {
            return new Errors.EmployeeDepartmentNotFoundError(query.Request.DepartmentId);
        }

        // return employees in department
        var employees = await _employeeRepository.GetByDepartmentId(departmentId, cancellationToken);
        return Result<IEnumerable<EmployeeResultResponse>>.Success(employees.Select(d => EmployeeResultResponse.FromDomain(d)));

    }
}
