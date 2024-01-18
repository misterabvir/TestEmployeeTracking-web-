using Core.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TokenService
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddTokenService(this IServiceCollection services, IConfiguration configuration)
        {
            var key = configuration.GetSection("Security").Value;
            if (string.IsNullOrEmpty(key)) throw new Exception("can't find security key");

            services.AddTransient<ITokenGenerator>(provider => new TokenGenerator(key));
            
            return services;
        } 
    }
}
