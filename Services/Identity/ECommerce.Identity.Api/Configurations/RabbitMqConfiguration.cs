using ECommerce.Identity.Api.Interfaces;
using ECommerce.Identity.Api.Services.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Identity.Api.Configurations
{
    public static class RabbitMqConfiguration
    {
        public static void AddRabbitMqConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICustomerRabbitMqClient, CustomerRabbitMqService>();
        }
    }
}
