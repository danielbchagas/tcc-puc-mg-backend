using ECommerce.Customer.Api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Customer.Api.Configurations
{
    public static class OptionsConfiguration
    {
        public static IServiceCollection AddOptionsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMqOption>(config => configuration.GetSection("RabbitMqOptions").Bind(config));
            services.Configure<JwtOption>(config => configuration.GetSection("JwtOptions").Bind(config));

            return services;
        }
    }
}
