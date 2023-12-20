using Entities.Abstractions.Services;
using Entities.Departments;
using Entities.Employees;
using Entities.Histories;
using Microsoft.Extensions.DependencyInjection;

namespace Entities;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IHistoryService, HistoryService>();
        return services;
    }
}