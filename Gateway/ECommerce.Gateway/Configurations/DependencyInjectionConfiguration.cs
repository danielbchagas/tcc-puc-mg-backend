using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Compras.Gateway.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));
        }
    }
}
