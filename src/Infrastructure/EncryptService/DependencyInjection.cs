using Core.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EncryptService;

public static class DependencyInjection
{
    public static IServiceCollection AddEncryptService(this IServiceCollection services)
    {
        services.AddTransient<IEncryption, Encryption>();
        
        return services;
    }
}
