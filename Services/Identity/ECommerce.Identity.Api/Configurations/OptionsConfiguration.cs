using ECommerce.Identity.Api.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Identity.Api.Configurations
{
    public static class OptionsConfiguration
    {
        public static void AddOptionsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMqOption>(config => configuration.GetSection("RabbitMqOptions").Bind(config));
            services.Configure<JwtOption>(config => configuration.GetSection("JwtOptions").Bind(config));
            services.Configure<GoogleOAuthOption>(config => configuration.GetSection("GoogleOauth").Bind(config));
            services.Configure<ServiceOption>(config => configuration.GetSection("ServiceOptions").Bind(config));
        }
    }
}
