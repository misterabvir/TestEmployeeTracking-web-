using Entities.Abstractions.Services;
using Entities.Departments;
using Entities.Employees;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationCore;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options =>
            options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        return services;
    }
}
