using ECommerce.WebApi.Core.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.WebApi.Core.Extensions
{
    public static class OptionsConfiguration
    {
        public static IServiceCollection AddOptionsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQOptions>(options => configuration.GetSection("RabbitMQ").Bind(options));

            return services;
        }
    }
}
