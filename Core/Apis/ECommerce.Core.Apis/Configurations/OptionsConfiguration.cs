using ECommerce.Core.Apis.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Core.Apis.Configurations
{
    public static class OptionsConfiguration
    {
        public static IServiceCollection AddOptionsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMq>(config => configuration.GetSection("RabbitMqOptions").Bind(config));
            services.Configure<Jwt>(config => configuration.GetSection("JwtOptions").Bind(config));
            services.Configure<Swagger>(config => configuration.GetSection("SwaggerOptions").Bind(config));

            return services;
        }
    }
}
