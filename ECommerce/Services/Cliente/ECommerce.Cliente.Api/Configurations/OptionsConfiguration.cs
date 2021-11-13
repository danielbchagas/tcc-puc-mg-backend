using ECommerce.Cliente.Api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Cliente.Api.Configurations
{
    public static class OptionsConfiguration
    {
        public static IServiceCollection AddOptionsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMqOptions>(config => configuration.GetSection("RabbitMqOptions").Bind(config));
            services.Configure<JwtOptions>(config => configuration.GetSection("JwtOptions").Bind(config));

            return services;
        }
    }
}
