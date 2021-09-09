using ECommerce.Compras.Gateway.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Compras.Gateway.Configurations
{
    public static class OptionsConfiguration
    {
        public static IServiceCollection AddOptionsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(config => configuration.GetSection("JwtOptions").Bind(config));
            services.Configure<ClienteServiceOptions>(config => configuration.GetSection("ClienteServiceOptions").Bind(config));

            return services;
        }
    }
}
