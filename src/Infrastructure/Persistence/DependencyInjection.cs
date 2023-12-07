using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Core.Abstractions.Repositories;
using Persistence.Common;
using Persistence.Repositories;
namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        string? connectionString = configuration.GetConnectionString("LocalDb");
        services.AddScoped((service) => new DbService(connectionString!));
        
        return services;
    }
}
