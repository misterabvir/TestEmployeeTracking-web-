using Entities.Abstractions.Services;
using Entities.Departments;
using Entities.Employees;
using Microsoft.Extensions.DependencyInjection;

namespace Entities;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        return services;
    }
}