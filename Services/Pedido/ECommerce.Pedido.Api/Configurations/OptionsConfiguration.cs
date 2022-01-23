using ECommerce.Pedido.Api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Pedido.Api.Configurations
{
    public static class OptionsConfiguration
    {
        public static void AddOptionsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOption>(config => configuration.GetSection("JwtOptions").Bind(config));
        }
    }
}
