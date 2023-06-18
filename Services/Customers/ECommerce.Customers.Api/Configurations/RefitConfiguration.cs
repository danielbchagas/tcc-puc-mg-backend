using ECommerce.Customers.Application.Services.REST;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;

namespace ECommerce.Customer.Api.Configurations
{
    public static class RefitConfiguration
    {
        public static IServiceCollection AddRefitConfiguration(this IServiceCollection services)
        {
            services
                .AddRefitClient<IViaCepService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://viacep.com.br/ws"));

            return services;
        }
    }
}
