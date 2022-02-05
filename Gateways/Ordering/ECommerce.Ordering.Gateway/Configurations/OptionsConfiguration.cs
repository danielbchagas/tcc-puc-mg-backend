using ECommerce.Ordering.Gateway.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Ordering.Gateway.Configurations
{
    public static class OptionsConfiguration
    {
        public static IServiceCollection AddOptionsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOption>(config => configuration.GetSection("JwtOptions").Bind(config));
            services.Configure<ServiceOption>(config => configuration.GetSection("ServiceOptions").Bind(config));

            return services;
        }
    }
}
