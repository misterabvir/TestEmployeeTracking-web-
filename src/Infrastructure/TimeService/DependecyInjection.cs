using ApplicationCore.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;

namespace TimeService;

public static class DependecyInjection
{
    public static IServiceCollection AddDateTimeService(this IServiceCollection services)
    {
        services.AddScoped<IDateTimeService, DateTimeService>();

        return services;
    }
}
