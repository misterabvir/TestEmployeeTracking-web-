using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ApplicationCore.Abstractions.Repositories;
using Persistence.Common;
using Persistence.Repositories;
namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("LocalDb");
        services.AddScoped((service) => new DbService(connectionString!));
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IHistoryRepository, HistoryRepository>();
        
        return services;
    }
}
