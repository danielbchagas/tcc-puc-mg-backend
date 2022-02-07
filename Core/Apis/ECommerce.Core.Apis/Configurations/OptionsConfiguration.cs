using ECommerce.Core.Apis.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Core.Apis.Configurations
{
    public static class OptionsConfiguration
    {
        public static IServiceCollection AddOptionsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQ>(config => configuration.GetSection("RabbitMQ").Bind(config));
            services.Configure<Jwt>(config => configuration.GetSection("JWT").Bind(config));

            return services;
        }
    }
}
